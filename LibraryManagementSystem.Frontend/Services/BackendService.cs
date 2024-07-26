using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Frontend.Services
{
    public class BackendService : ServiceBase
    {
        public BackendService() : base() { }

        public async void TerminateProcessAsync()
        {
            try
            {
                await this.Client.PostAsync("backend/terminate", null);
            } catch (Exception)
            {
                throw new Exception("Error terminating backend process");
            }
        }
    }
}
