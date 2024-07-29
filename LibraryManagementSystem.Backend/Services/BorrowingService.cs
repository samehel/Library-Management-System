using LibraryManagementSystem.Backend.Contexts;
using LibraryManagementSystem.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Backend.Services
{
    public class BorrowingService : IBorrowingService
    {

        private readonly LibraryContext _context;

        public BorrowingService(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<Borrowing?> CreateBorrowRequestAsync(Borrowing borrowing)
        {
            this._context.Borrowing.Add(borrowing);
            await this._context.SaveChangesAsync();
            return borrowing;
        }

        public async Task<List<Borrowing>> GetAllBorrowRequestsAsync()
        {
            return await this._context.Borrowing.ToListAsync();
        }

        public async Task<Borrowing?> GetBorrowRequestAsync(int borrowID)
        { 
            return await this._context.Borrowing.FindAsync(borrowID);
        }

        public async Task<Borrowing?> UpdateBorrowRequestAsync(int borrowID, bool? renewReturnDate, bool? applyLateFee, bool? returned)
        {
            Borrowing? borrowing = await GetBorrowRequestAsync(borrowID);

            if(borrowing != null)
            {
                if (renewReturnDate != null && renewReturnDate != false)
                {
                    borrowing.ReturnDate = borrowing.ReturnDate!.Value.AddDays(7);
                    borrowing.RenewalCount += 1;
                }

                if (returned != null && returned != false)
                {
                    borrowing.Returned = true;
                    borrowing.ReturnDate = DateTime.Now;
                }

                if (applyLateFee != null && applyLateFee != false)
                    borrowing.LateFee += 5.0;
            }

            await this._context.SaveChangesAsync();
            return borrowing;
        }

        public async Task<List<Borrowing>> GetUserBorrowRequestsAsync(int userID)
        {
            return await this._context.Borrowing.Where(b => b.UserID == userID).ToListAsync();
        }
    }
}
