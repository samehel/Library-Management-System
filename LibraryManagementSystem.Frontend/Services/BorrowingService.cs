using LibraryManagementSystem.Frontend.Models;
using LibraryManagementSystem.Frontend.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Frontend.Services
{
    public class BorrowingService : ServiceBase
    {
        public BorrowingService(): base()
        {
            if (MainWindow.UserToken != null)
                if (!string.IsNullOrEmpty(MainWindow.UserToken.TokenValue))
                    this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainWindow.UserToken.TokenValue);
        }

        public async Task<Borrowing> CreateBorrowRequestAsync(Borrowing borrowRequest)
        {
            try
            {
                string json = JsonConvert.SerializeObject(borrowRequest);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PostAsync("borrowing/CreateRequest", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }

                Borrowing borrowing = await response.Content.ReadAsAsync<Borrowing>();
                return borrowing;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<List<Borrowing>> CreateBorrowRequestsAsync(List<Borrowing> borrowRequests)
        {
            try
            {
                string json = JsonConvert.SerializeObject(borrowRequests);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PostAsync("borrowing/CreateRequests", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }

                List<Borrowing> borrowings = await response.Content.ReadAsAsync<List<Borrowing>>();
                return borrowings;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<List<Borrowing>> GetAllBorrowRequestsAsync()
        {
            try
            {
                HttpResponseMessage response = await this.Client.GetAsync("borrowing");

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                List<Borrowing> borrowRequests = await response.Content.ReadAsAsync<List<Borrowing>>();
                return borrowRequests;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all borrow requests", ex);
            }
        }

        public async Task<Borrowing> GetBorrowRequestAsync(int borrowID)
        {
            try
            {
                HttpResponseMessage response = await this.Client.GetAsync($"borrowing/{borrowID}");
                response.EnsureSuccessStatusCode();
                Borrowing borrowRequest = await response.Content.ReadAsAsync<Borrowing>();
                return borrowRequest;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting borrow request by ID", ex);
            }
        }

        public async Task<Borrowing> UpdateBorrowRequestAsync(int borrowID, bool renewReturnDate, bool applyLateFee, bool returned)
        {
            try
            {
                var borrowUpdateDTO = new BorrowUpdateDTO
                {
                    RenewReturnDate = renewReturnDate,
                    ApplyLateFee = applyLateFee,
                    Returned = returned
                };

                string json = JsonConvert.SerializeObject(borrowUpdateDTO);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.Client.PutAsync($"borrowing/{borrowID}", content);

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                Borrowing borrowRequest = await response.Content.ReadAsAsync<Borrowing>();
                return borrowRequest;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating a borrow request", ex);
            }
        }

        public async Task<List<Borrowing>> GetUserBorrowRequestsAsync(int userID)
        {
            try
            {
                HttpResponseMessage response = await this.Client.GetAsync($"borrowing/user/{userID}");
                response.EnsureSuccessStatusCode();
                List<Borrowing> userBorrowRequests = await response.Content.ReadAsAsync<List<Borrowing>>();
                return userBorrowRequests;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting user borrow requests", ex);
            }
        }
    }

    public class BorrowUpdateDTO
    {
        public bool RenewReturnDate { get; set; }
        public bool ApplyLateFee { get; set; }
        public bool Returned { get; set; }
    }
}
