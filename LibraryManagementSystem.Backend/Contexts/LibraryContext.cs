using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Backend.Contexts
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) 
        { 
            
        }


    }
}
