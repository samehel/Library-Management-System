using LibraryManagementSystem.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Backend.Controllers
{
    [ApiController]
    [Route("api/backend")]
    public class BackendController : ControllerBase
    {

        private readonly IBackendService _service; 

        public BackendController(IBackendService service)
        {
            this._service = service;  
        }

        [HttpPost("terminate")]
        public void TerminateProcess()
        {
            this._service.TerminateProcess();
        }
    }
}
