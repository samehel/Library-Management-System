using LibraryManagementSystem.Frontend.Enums;

namespace LibraryManagementSystem.Frontend.Models
{
    public class Fine
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public double Amount { get; set; }
        public string Reason { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
