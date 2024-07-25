using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Frontend.Models;

namespace LibraryManagementSystem.Frontend.Services
{
    public class BookService : ServiceBase
    {
        public BookService(): base() { }

        public async Task<Book> GetBookByIDAsync(int bookID, string token)
        {
            try
            {
                this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await this.Client.GetAsync($"books/{bookID}");
                response.EnsureSuccessStatusCode();
                Book book = await response.Content.ReadAsAsync<Book>();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting book by ID", ex);
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            try
            {
                HttpResponseMessage response = await this.Client.GetAsync("books");

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                List<Book> books = await response.Content.ReadAsAsync<List<Book>>();
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all books", ex);
            }
        }

        public async Task<Book> CreateBookAsync(Book newBook, string token)
        {
            try
            {
                this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string json = JsonConvert.SerializeObject(newBook);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PostAsync("books", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }

                Book book = await response.Content.ReadAsAsync<Book>();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Book> UpdateBookAsync(int bookID, Book updatedBook, string token)
        {
            try
            {
                this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string json = JsonConvert.SerializeObject(updatedBook);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PutAsync($"books/{bookID}", content);

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                Book book = await response.Content.ReadAsAsync<Book>();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating a book", ex);
            }
        }

        public async Task<string> DeleteBookAsync(int bookID, string token)
        {
            try
            {
                this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await this.Client.DeleteAsync($"books/{bookID}");

                if (response.StatusCode == HttpStatusCode.Forbidden)
                    return "You lack the permissions to carry out this request";

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return "The book you are attempting to delete was not found";

                response.EnsureSuccessStatusCode();
                return $"Successfully deleted user with ID: {bookID}";
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting book by ID", ex);
            }
        }
    }
}
