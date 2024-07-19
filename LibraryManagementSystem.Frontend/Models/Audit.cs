using System;
using LibraryManagementSystem.Frontend.Utilities.Enums;

namespace LibraryManagementSystem.Frontend.Models
{
    public class Audit
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public ActionType ActionType { get; set; }
        public DateTime ActionDate { get; private set; } = DateTime.Now;
        public string Details { get; set; }
        public bool isDeleted { get; set; }

    }
}
