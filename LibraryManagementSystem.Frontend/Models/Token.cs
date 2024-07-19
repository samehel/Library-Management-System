using System;
using System.Text;
using System.Security.Claims;

namespace LibraryManagementSystem.Frontend.Models
{
    public class Token
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string TokenValue { get; set; }
        public DateTime Expiration { get; set; }
    }
}
