﻿using System;

namespace LibraryManagementSystem.Frontend.Models
{
    public class Borrowing
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RenewalCount { get; set; }
        public double LateFee { get; set; }
        public bool Returned { get; set; }

    }
}
