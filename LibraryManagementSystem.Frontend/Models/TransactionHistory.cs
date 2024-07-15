using LibraryManagementSystem.Frontend.Enums;
using System;

namespace LibraryManagementSystem.Frontend.Models
{
    public class TransactionHistory
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate => DateTime.Now;
    }
}
