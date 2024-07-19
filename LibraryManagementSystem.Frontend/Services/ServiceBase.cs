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
        private readonly HttpClient _client;

        public ServiceBase()
        {
            this._client = new HttpClient{
                BaseAddress = new Uri("http://localhost:5109/api/")
            };
        }

        public HttpClient Client { get { return this._client; } }
    }
}
