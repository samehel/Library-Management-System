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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartBook> CartBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartBook>()
                .HasKey(cb => new { cb.CartID, cb.BookID });

            modelBuilder.Entity<CartBook>()
                .HasOne(cb => cb.Cart)
                .WithMany(c => c.CartBooks)
                .HasForeignKey(cb => cb.CartID);

            modelBuilder.Entity<CartBook>()
                .HasOne(cb => cb.Book)
                .WithMany(b => b.CartBooks)
                .HasForeignKey(cb => cb.BookID);

            base.OnModelCreating(modelBuilder);
        }

    }
}
