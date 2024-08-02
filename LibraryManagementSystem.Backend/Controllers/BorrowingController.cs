using LibraryManagementSystem.Backend.DTOs;
using LibraryManagementSystem.Backend.Enums;
using LibraryManagementSystem.Backend.Models;
using LibraryManagementSystem.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Backend.Controllers
{
    [ApiController]
    [Route("api/borrowing")]
    [Authorize]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IAuditService _auditService;

        public BorrowingController(IBorrowingService borrowingService, IAuditService auditService)
        {
            this._borrowingService = borrowingService;
            this._auditService = auditService;
        }

        [HttpPost("CreateRequest")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Borrowing>> CreateBorrowRequest(Borrowing borrowing)
        {
            try
            {
                Borrowing? borrowed = await this._borrowingService.CreateBorrowRequestAsync(borrowing);

                if (borrowed == null)
                    return BadRequest();

                await this._auditService.CreateAuditAsync(new Audit
                {
                    UserID = borrowing.UserID,
                    ActionType = ActionType.BORROW_REQUEST_CREATED.ToString(),
                    Details = $"User with ID {borrowing.UserID} has borrowed book with ID {borrowing.BookID}",
                    isDeleted = false
                });

                return Ok(borrowed);
            } catch (Exception)
            {
                throw new Exception("Failed to create a new borrow request");
            }
        }

        [HttpPost("CreateRequests")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<List<Borrowing>>> CreateBorrowRequests(List<Borrowing> borrowings)
        {
            try
            {
                List<Borrowing>? borrowed = await this._borrowingService.CreateBorrowRequestsAsync(borrowings);

                if (borrowed == null)
                    return BadRequest();

                string bookIDs = string.Empty;
                foreach (var borrowing in borrowed)
                    if (borrowing != borrowed[borrowed.Count - 1])
                        bookIDs += borrowing.ID + ", ";
                    else
                        bookIDs += borrowing.ID;

                await this._auditService.CreateAuditAsync(new Audit
                {
                    UserID = borrowed[0].UserID,
                    ActionType = ActionType.BORROW_REQUEST_CREATED.ToString(),
                    Details = $"User with ID {borrowed[0].UserID} has borrowed books with IDs {bookIDs}",
                    isDeleted = false
                });

                return Ok(borrowed);
            }
            catch (Exception)
            {
                throw new Exception("Failed to create a new borrow requests");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Borrowing>>> GetAllBorrowRequests()
        {
            try
            {
                List<Borrowing> borrowedList = await this._borrowingService.GetAllBorrowRequestsAsync();

                if (borrowedList == null || borrowedList.Count == 0)
                    return NotFound();

                await this._auditService.CreateAuditAsync(new Audit
                {
                    UserID = 0,
                    ActionType = ActionType.BORROW_REQUESTS_RETRIEVED.ToString(),
                    Details = $"Admin has requested to view a list of all borrowed books",
                    isDeleted = false
                });

                return Ok(borrowedList);
            } catch (Exception)
            {
                throw new Exception("Failed to retrieve all borrow requests");
            }
        }

        [HttpGet("{borrowID}")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Borrowing>> GetBorrowRequest(int borrowID)
        {
            try
            {
                Borrowing? borrowing = await this._borrowingService.GetBorrowRequestAsync(borrowID);

                if(borrowing == null)
                    return NotFound();

                await this._auditService.CreateAuditAsync(new Audit
                {
                    UserID = borrowing.UserID,
                    ActionType = ActionType.BORROW_REQUEST_RETRIEVED.ToString(),
                    Details = $"User with ID {borrowing.UserID} has requested to view a borrow request with ID {borrowID}",
                    isDeleted = false
                });

                return Ok(borrowing);
            } catch (Exception)
            {
                throw new Exception("Failed to retrieve borrow request");
            }
        }

        [HttpPut("{borrowID}")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Borrowing>> UpdateBorrowRequest(int borrowID, [FromBody] BorrowUpdateDTO borrowUpdateDTO)
        {
            try
            {
                Borrowing? borrowing = await this._borrowingService.UpdateBorrowRequestAsync(borrowID, borrowUpdateDTO.RenewReturnDate, borrowUpdateDTO.ApplyLateFee, borrowUpdateDTO.Returned);
            
                if(borrowing == null)
                    return NotFound();

                await this._auditService.CreateAuditAsync(new Audit
                {
                    UserID = borrowing.UserID,
                    ActionType = borrowUpdateDTO.Returned!.Value ? ActionType.RETURN_BOOK.ToString() : ActionType.BORROW_REQUEST_UPDATED.ToString(),
                    Details = $"User with ID {borrowing.UserID} borrowing book with ID {borrowing.BookID} has their borrow request updated",
                    isDeleted = false
                });

                return Ok(borrowing);
            } catch (Exception)
            {
                throw new Exception("Failed to update borrow request");
            }
        }

        [HttpGet("user/{userID}")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<List<Borrowing>>> GetUserBorrowRequests(int userID)
        {
            try
            {
                List<Borrowing> borrowings = await this._borrowingService.GetUserBorrowRequestsAsync(userID);

                if(borrowings == null || borrowings.Count == 0)
                    return NotFound();

                await this._auditService.CreateAuditAsync(new Audit
                {
                    UserID = userID,
                    ActionType = ActionType.BORROW_REQUESTS_USER_SPECIFIC_RETRIEVED.ToString(),
                    Details = $"User with ID {userID} requested to view all their borrow history",
                    isDeleted = false
                });

                return Ok(borrowings);
            }
            catch (Exception)
            {
                throw new Exception("Failed to retrieve borrow requests for user");
            }
        }
    }
}
