namespace LibraryManagementSystem.Backend.DTOs
{
    public class CartDTO
    {
        public int? userID {  get; set; }
        public int? bookID { get; set; }
        public int? quantity { get; set; }
    }
}
