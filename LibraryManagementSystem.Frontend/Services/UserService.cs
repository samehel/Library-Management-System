using LibraryManagementSystem.Frontend.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using System.Windows;

namespace LibraryManagementSystem.Frontend.Services
{
    public class UserService : ServiceBase
    {
        public UserService() : base() { }

        public async Task<User> GetUserByIDAsync(int userID)
        {
            try
            {
                HttpResponseMessage response = await this.Client.GetAsync($"users/{userID}");
                response.EnsureSuccessStatusCode();
                User user = await response.Content.ReadAsAsync<User>();
                return user;
            } catch (Exception ex)
            {
                throw new Exception("Error getting user by ID", ex);
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                HttpResponseMessage response = await this.Client.GetAsync("users");

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                List<User> users = await response.Content.ReadAsAsync<List<User>>();
                return users;
            } catch (Exception ex)
            {
                throw new Exception("Error getting all users", ex);
            }
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            try
            {
                string json = JsonConvert.SerializeObject(newUser);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PostAsync("users", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }

                User user = await response.Content.ReadAsAsync<User>();
                return user;
            } 
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<User> UpdateUserAsync(int userID, User updatedUser, string token)
        {
            try
            {
                this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string json = JsonConvert.SerializeObject(updatedUser);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PutAsync($"users/{userID}", content);

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                User user = await response.Content.ReadAsAsync<User>();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating a user", ex);
            }
        }

        public async Task<string> DeleteUserAsync(int userID, string token)
        {
            try
            {
                this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await this.Client.DeleteAsync($"users/{userID}");

                if (response.StatusCode == HttpStatusCode.Forbidden)
                    return "You lack the permissions to carry out this request";

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return "The user you are attempting to delete was not found";

                response.EnsureSuccessStatusCode();
                return $"Successfully deleted user with ID: {userID}";
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting user by ID", ex);
            }
        }

    }
}
