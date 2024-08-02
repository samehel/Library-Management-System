namespace LibraryManagementSystem.Backend.DTOs
{
    public class BorrowUpdateDTO
    {
        public bool? RenewReturnDate { get; set; }
        public bool? ApplyLateFee { get; set; }
        public bool? Returned { get; set; }
    }
}
