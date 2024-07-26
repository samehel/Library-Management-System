using LibraryManagementSystem.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LibraryManagementSystem.Frontend.Utilities
{
    public static class ImageCache
    {
        private static readonly Dictionary<string, BitmapImage> _cache = new Dictionary<string, BitmapImage>();
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task DownloadAndCacheImagesAsync(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                if (!string.IsNullOrEmpty(book.PictureUrl) && !_cache.ContainsKey(book.PictureUrl))
                {
                    var image = await DownloadImageAsync(book.PictureUrl);
                    if (image != null)
                    {
                        _cache[book.PictureUrl] = image;
                    }
                }
            }
        }

        private static async Task<BitmapImage> DownloadImageAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        public static BitmapImage GetImage(string url)
        {
            _cache.TryGetValue(url, out var image);
            return image;
        }
    }
}
