using LibraryManagementSystem.Frontend.Enums;
using System;

namespace LibraryManagementSystem.Frontend.Models
{
    public class Audit
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public ActionType ActionType { get; set; }
        public DateTime ActionDate => DateTime.Now;
        public string Details { get; set; }
    }
}
