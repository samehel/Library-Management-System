namespace LibraryManagementSystem.Backend.DTOs
{
    public class BorrowUpdateDTO
    {
        public bool? renewReturnDate { get; set; }
        public bool? applyLateFee { get; set; }
        public bool? Returned { get; set; }
    }
}
