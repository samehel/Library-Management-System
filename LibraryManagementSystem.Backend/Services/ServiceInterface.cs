using LibraryManagementSystem.Backend.DTOs;
using LibraryManagementSystem.Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Backend.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User?> GetUserByIDAsync(int userID);
        Task<List<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(int userID, User updatedUser);
        Task<bool> DeleteUserAsync(int userID);
    }

    public interface ITokenService
    {
        Task<Token> CreateTokenAsync(User user);
    }

    public interface IAuditService
    {
        Task<Audit> CreateAuditAsync(Audit audit);
        Task<bool> DeleteAuditAsync(int auditID);
        Task<Audit?> GetAuditAsync(int auditID);
        Task<List<Audit>> GetAllAuditsAsync();
        Task<List<Audit>> GetAllDeletedAuditsAsync();
    }

    public interface IBookService
    {
        Task<Book> CreateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int bookID);
        Task<Book?> GetBookAsync(int bookID);
        Task<Book?> UpdateBookAsync(int bookID, Book updatedBook);
        Task<List<Book>> GetAllBooksAsync();
    }

    public interface IBackendService
    {
        void TerminateProcess();
    }

    public interface ICartService
    {
        Task<Cart> AddBookToCartAsync(int userID, int bookID);
        Task<Cart> RemoveBookFromCartAsync(int userID, int bookID);
        Task<Cart> ClearCartAsync(int userID);
        Task<Cart> UpdateCartBookQuantityAsync(int userID, int bookID, int quantity);
        Task<Cart> GetOrCreateCartAsync(int userID);
    }

    public interface ICartBookService
    {
        Task<List<CartBook>> GetAllCartBooksAsync();
    }

    public interface IBorrowingService
    {
        Task<Borrowing?> CreateBorrowRequestAsync(Borrowing borrowing);
        Task<List<Borrowing>?> CreateBorrowRequestsAsync(List<Borrowing> borrowings);
        Task<Borrowing?> GetBorrowRequestAsync(int borrowID);
        Task<Borrowing?> UpdateBorrowRequestAsync(int borrowID, bool? renewReturnDate, bool? applyLateFee, bool? returned);
        Task<List<Borrowing>> GetAllBorrowRequestsAsync();
        Task<List<Borrowing>> GetUserBorrowRequestsAsync(int userID);

    }
}
