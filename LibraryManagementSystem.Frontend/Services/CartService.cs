using LibraryManagementSystem.Frontend.Models;
using LibraryManagementSystem.Frontend.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Frontend.Services
{
    public class CartService : ServiceBase
    {
        public CartService() : base()
        {
            if (MainWindow.UserToken != null)
                if (!string.IsNullOrEmpty(MainWindow.UserToken.TokenValue))
                    this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainWindow.UserToken.TokenValue);
        }

        public async Task<Cart> AddToCartAsync(int userID, int bookID)
        {
            try
            {
                var cartDto = new CartDTO
                {
                    userID = userID,
                    bookID = bookID
                };

                string json = JsonConvert.SerializeObject(cartDto);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PostAsync("cart/AddToCart", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }

                Cart cart = await response.Content.ReadAsAsync<Cart>();
                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding book to cart", ex);
            }
        }

        public async Task<Cart> RemoveFromCartAsync(int userID, int bookID)
        {
            try
            {
                var cartDto = new CartDTO
                {
                    userID = userID,
                    bookID = bookID
                };

                string json = JsonConvert.SerializeObject(cartDto);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PostAsync("cart/RemoveFromCart", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }

                Cart cart = await response.Content.ReadAsAsync<Cart>();
                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing book from cart", ex);
            }
        }

        public async Task<Cart> ClearCartAsync(int userID)
        {
            try
            {
                var cartDto = new CartDTO
                {
                    userID = userID,
                };

                string json = JsonConvert.SerializeObject(cartDto);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PostAsync("cart/ClearCart", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }

                Cart cart = await response.Content.ReadAsAsync<Cart>();
                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing book from cart", ex);
            }
        }

        public async Task UpdateCartBookQuantityAsync(int userID, int bookID, int quantity)
        {
            try
            {
                var cartDto = new CartDTO
                {
                    userID = userID,
                    bookID = bookID,
                    quantity = quantity
                };

                string json = JsonConvert.SerializeObject(cartDto);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PostAsync("cart/UpdateCartBookQuantity", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating cart book quantity", ex);
            }
        }

        public async Task<Cart> GetCartAsync(int userID)
        {
            HttpResponseMessage response = await this.Client.GetAsync($"cart/{userID}");

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(errorContent);
            }

            Cart cart = await response.Content.ReadAsAsync<Cart>();
            return cart;
        }

    }

    public class CartDTO
    {
        public int? userID { get; set; }
        public int? bookID { get; set; }
        public int? quantity { get; set; }
    }
}
