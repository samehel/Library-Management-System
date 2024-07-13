using System;

namespace LibraryManagementSystem.Frontend.Models
{
    public class Borrowing
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate => this.BorrowDate.AddDays(7);
        public int RenewalCount { get; set; }
        public double LateFee { get; set; }
    }
}
