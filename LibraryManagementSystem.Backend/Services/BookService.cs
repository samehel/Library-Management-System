using LibraryManagementSystem.Backend.Contexts;
using LibraryManagementSystem.Backend.Models;
using LibraryManagementSystem.Backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Backend.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            this._context = context;
        }

        private static bool AreRequiredDeweyAttributesPresent(Book book)
        {
            if (string.IsNullOrEmpty(book.Title)
                || string.IsNullOrEmpty(book.Description)
                || string.IsNullOrEmpty(book.Author)
                || book.Genre == null)
                return false;

            return true;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            Book? foundBook = await this._context.Books.FirstOrDefaultAsync(b => b.ISBN == book.ISBN);

            if (foundBook != null)
                throw new Exception("Error: Duplicate ISBN.");

            if (AreRequiredDeweyAttributesPresent(book))
                throw new Exception("Error: Book info is incomplete");

            book.DeweyDecimalNumber = DeweyDecimalNumberGeneratorUtil.GenerateDeweyDecimalNumber(book);

            this._context.Books.Add(book);
            await this._context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBookAsync(int bookID)
        {
            Book? book = await this._context.Books.FindAsync(bookID);

            if (book != null)
            {
                this._context.Books.Remove(book);
                await this._context.SaveChangesAsync();
                return true;
            }

            return true;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await this._context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookAsync(int bookID)
        {
            return await this._context.Books.FindAsync(bookID);
        }

        public async Task<Book?> UpdateBookAsync(int bookID, Book updatedBook)
        {
            Book? book = await this._context.Books.FindAsync(bookID);

            if (book != null)
            {
                if (!string.IsNullOrEmpty(updatedBook.Title))
                    book.Title = updatedBook.Title;

                if(!string.IsNullOrEmpty(updatedBook.Description))
                    book.Description = updatedBook.Description;

                if(!string.IsNullOrEmpty(updatedBook.Author))
                    book.Author = updatedBook.Author;

                if(!string.IsNullOrEmpty(updatedBook.ISBN))
                    book.ISBN = updatedBook.ISBN;

                if(updatedBook.Genre != null)
                    book.Genre = updatedBook.Genre;

                if(!(updatedBook.Quantity < 0))
                    book.Quantity = updatedBook.Quantity;

                if(!string.IsNullOrEmpty(updatedBook.PictureUrl))
                    book.PictureUrl = updatedBook.PictureUrl;

                if(AreRequiredDeweyAttributesPresent(book))
                    book.DeweyDecimalNumber = updatedBook.DeweyDecimalNumber;

                await this._context.SaveChangesAsync();
            }

            return book;
        }
    }
}
