using System;

namespace LibraryManagementSystem.Frontend.Models
{
    public class Borrowing
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.MinValue;
        public DateTime ReturnDate { get { return BorrowDate != DateTime.MinValue ? BorrowDate.AddDays(7) : DateTime.MinValue; } private set { } }
        public int RenewalCount { get; set; }
        public double LateFee { get; set; }
    }
}
