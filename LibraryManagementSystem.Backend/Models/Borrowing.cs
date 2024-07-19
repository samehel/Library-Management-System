﻿namespace LibraryManagementSystem.Backend.Models
{
    public class Borrowing
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get { return BorrowDate != null ? BorrowDate.Value.AddDays(7) : null; } private set { } }
        public int RenewalCount { get; set; }
        public double LateFee { get; set; }
    }
}
