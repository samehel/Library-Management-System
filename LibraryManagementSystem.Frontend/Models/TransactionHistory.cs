using System;
using LibraryManagementSystem.Frontend.Utilities.Enums;

namespace LibraryManagementSystem.Frontend.Models
{
    public class TransactionHistory
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; private set; } = DateTime.Now;
    }
}
