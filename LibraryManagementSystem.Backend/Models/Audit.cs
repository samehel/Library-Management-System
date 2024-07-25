using LibraryManagementSystem.Backend.Enums;

namespace LibraryManagementSystem.Backend.Models
{
    public class Audit
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string? ActionType { get; set; }
        public DateTime ActionDate { get; private set; } = DateTime.Now;
        public string? Details { get; set; }
        public bool isDeleted { get; set; }

    }
}
