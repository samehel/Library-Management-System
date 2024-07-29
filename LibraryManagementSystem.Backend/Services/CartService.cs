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

        public async Task<Cart> GetOrCreateCartAsync(int userID)
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

        public async Task<Cart> AddBookToCartAsync(int userID, int bookID)
        {
            Cart cart = await GetOrCreateCartAsync(userID);
            Book? book = await this._context.Books.FindAsync(bookID) ?? throw new Exception("The book you are trying to add to your cart does not exist");

            if (cart.CartBooks != null && cart.CartBooks.Any(cb => cb.BookID == bookID))
            {
                var cartBook = cart.CartBooks.FirstOrDefault(cb => cb.BookID == bookID);
                if (cartBook == null)
                    throw new Exception("Update failed, cart does not contain any books");

                cartBook.Quantity += 1;
                this._context.CartBooks.Update(cartBook);
                await this._context.SaveChangesAsync();
            }
            else
            {
                CartBook cartBook = new CartBook
                {
                    CartID = cart.ID,
                    BookID = book.ID,
                    Cart = cart,
                    Book = book,
                    Quantity = 1
                };

                cart.CartBooks!.Add(cartBook);
            }
            await this._context.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> ClearCartAsync(int userID)
        {
            Cart cart = await GetOrCreateCartAsync(userID);
            cart.CartBooks!.Clear();
            await this._context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> RemoveBookFromCartAsync(int userID, int bookID)
        {
            Cart cart = await GetOrCreateCartAsync(userID);
            CartBook cartBook = cart.CartBooks!.FirstOrDefault(cb => cb.BookID == bookID) ?? throw new Exception("The book you are trying to add to your cart does not exist");
        
            cart.CartBooks!.Remove(cartBook);
            await this._context.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> UpdateCartBookQuantityAsync(int userID, int bookID, int quantity)
        {
            var cart = _context.Carts.Include(c => c.CartBooks).FirstOrDefault(c => c.UserID == userID);

            if (cart == null)
                throw new Exception("Update failed, cart does not exist");

            var cartBook = cart.CartBooks!.FirstOrDefault(cb => cb.BookID == bookID);
            if (cartBook == null)
                throw new Exception("Update failed, cart does not contain any books");

            cartBook.Quantity = quantity;
            this._context.CartBooks.Update(cartBook);
            await this._context.SaveChangesAsync();

            return cart;
        }

    }
}
