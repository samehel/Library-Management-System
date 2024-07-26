using System;
using System.Diagnostics;

namespace LibraryManagementSystem.Backend.Services
{
    public class BackendService : IBackendService
    {
        public void TerminateProcess()
        {
            Process.GetCurrentProcess().Close();
            Process.GetCurrentProcess().Kill();
        }
    }
}
