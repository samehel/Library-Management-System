using LibraryManagementSystem.Backend.Contexts;
using LibraryManagementSystem.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Backend.Services
{
    public class CartBookService : ICartBookService
    {

        private readonly LibraryContext _context;

        public CartBookService(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<List<CartBook>> GetAllCartBooks()
        {
            return await this._context.CartBooks.ToListAsync();
        }
    }
}
