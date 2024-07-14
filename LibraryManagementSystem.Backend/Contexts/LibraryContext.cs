using LibraryManagementSystem.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Backend.Contexts
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Audit> Audits { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowing { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}
