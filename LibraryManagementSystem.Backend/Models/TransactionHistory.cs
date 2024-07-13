using LibraryManagementSystem.Backend.Enums;

namespace LibraryManagementSystem.Backend.Models
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
