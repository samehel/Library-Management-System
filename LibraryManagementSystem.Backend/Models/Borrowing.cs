namespace LibraryManagementSystem.Backend.Models
{
    public class Borrowing
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public DateTime? BorrowDate { get; set; } = DateTime.MinValue;
        public DateTime? ReturnDate { get { return BorrowDate != null ? BorrowDate.Value.AddDays(7) : null; } set { } }
        public int RenewalCount { get; set; } = 0;
        public double LateFee { get; set; } = 0;
        public bool Returned { get; set; } = false;
    }
}
