using LibraryManagementSystem.Backend.Contexts;
using LibraryManagementSystem.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Backend.Services
{
    public class CartService : ICartService
    {
        private readonly LibraryContext _context;

        public CartService(LibraryContext context)
        {
            this._context = context;
        }

        private async Task<Cart> GetOrCreateCart(int userID)
        {
            Cart? cart = this._context.Carts?.Include(c => c.CartBooks)!
                                           .ThenInclude(cb => cb.Book)
                                           .FirstOrDefault(c => c.UserID == userID);

            if (cart == null)
            {
                cart = new Cart
                {
                    ID = Guid.NewGuid(),
                    UserID = userID,
                    CartBooks = new List<CartBook>()
                };

                this._context.Carts?.Add(cart);
                await this._context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<Cart> AddBookToCart(int userID, int bookID)
        {
            Cart cart = await GetOrCreateCart(userID);
            Book? book = await this._context.Books.FindAsync(bookID) ?? throw new Exception("The book you are trying to add to your cart does not exist");

            CartBook cartBook = new CartBook
            {
                CartID = cart.ID,
                BookID = book.ID,
                Cart = cart,
                Book = book
            };

            cart.CartBooks!.Add(cartBook);
            await this._context.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> ClearCart(int userID)
        {
            Cart cart = await GetOrCreateCart(userID);
            cart.CartBooks!.Clear();
            await this._context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> RemoveBookFromCart(int userID, int bookID)
        {
            Cart cart = await GetOrCreateCart(userID);
            CartBook cartBook = cart.CartBooks!.FirstOrDefault(cb => cb.BookID == bookID) ?? throw new Exception("The book you are trying to add to your cart does not exist");
        
            cart.CartBooks!.Remove(cartBook);
            await this._context.SaveChangesAsync();

            return cart;
        }
    }
}
