namespace LibraryManagementSystem.Backend.Models
{
    public class CartBook
    {
        public Guid CartID { get; set; }
        public Cart? Cart { get; set; }

        public int BookID { get; set; }
        public Book? Book { get; set; }
    }
}
