using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Frontend.Services
{
    public class ServiceBase
    {
        protected readonly HttpClient _httpClient;

        public ServiceBase(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri("http://localhost:5109/api/");
        }
    }
}
