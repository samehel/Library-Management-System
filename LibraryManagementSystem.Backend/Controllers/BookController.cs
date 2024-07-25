using LibraryManagementSystem.Backend.Models;
using LibraryManagementSystem.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Backend.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuditService _auditService;
        private readonly IUserService _userService;
        
        public BookController(IBookService bookService, IAuditService auditService, IUserService userService)
        {
            this._bookService = bookService;
            this._auditService = auditService;
            this._userService = userService;    
        }

        [HttpGet("{bookID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> GetBookByID(int bookID)
        {
            var invokedUserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            User? invokedUser = await this._userService.GetUserByIDAsync(int.Parse(invokedUserID!));

            if (string.IsNullOrEmpty(invokedUser?.Role) || !invokedUser.Role.Equals("Admin"))
                return Forbid();

            Book? book = await this._bookService.GetBookAsync(bookID);

            await this._auditService.CreateAuditAsync(new Audit
            {
                UserID = invokedUser!.ID,
                ActionType = Enums.ActionType.GET_BOOK.ToString(),
                Details = $"Admin {invokedUser.Username} has requested to view book with ID {bookID}.",
                isDeleted = false
            });

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetAllBooks()
        {
            List<Book> books = await this._bookService.GetAllBooksAsync();

            if (books.Count == 0 || books == null)
                return NotFound();

            return Ok(books);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            try
            {
                var invokedUserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                User? invokedUser = await this._userService.GetUserByIDAsync(int.Parse(invokedUserID!));

                if (string.IsNullOrEmpty(invokedUser?.Role) || !invokedUser.Role.Equals("Admin"))
                    return Forbid();

                Book createdBook = await this._bookService.CreateBookAsync(book);

                await this._auditService.CreateAuditAsync(new Audit
                {
                    UserID = 0,
                    ActionType = Enums.ActionType.CREATE_BOOK.ToString(),
                    Details = $"New Book created with Title \"{book.Title}\\\"\" and ISBN \"{book.ISBN}\"",
                    isDeleted = false

                });

                return CreatedAtAction(nameof(GetBookByID), new { bookID = createdBook.ID }, createdBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{bookID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> UpdateBook(int bookID, Book updatedBook)
        {
            var invokedUserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            User? invokedUser = await this._userService.GetUserByIDAsync(int.Parse(invokedUserID!));

            if (string.IsNullOrEmpty(invokedUser?.Role) || !invokedUser.Role.Equals("Admin"))
                return Forbid();

            Book? book = await this._bookService.UpdateBookAsync(bookID, updatedBook);

            if (book == null)
                return NotFound();

            await this._auditService.CreateAuditAsync(new Audit
            {
                UserID = invokedUser.ID,
                ActionType = Enums.ActionType.UPDATE_BOOK.ToString(),
                Details = $"\"{invokedUser.Username}\" updated book info for book with ID \"{updatedBook.ID}\"",
                isDeleted = false
            });

            return Ok(book);
        }

        [HttpDelete("${bookID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> DeleteBook(int bookID)
        {
            var invokedUserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            User? invokedUser = await this._userService.GetUserByIDAsync(int.Parse(invokedUserID!));

            if (string.IsNullOrEmpty(invokedUser?.Role) || !invokedUser.Role.Equals("Admin"))
                return Forbid();

            bool isDeleted = await this._bookService.DeleteBookAsync(bookID);

            if (!isDeleted)
                return NotFound();

            await this._auditService.CreateAuditAsync(new Audit
            {
                UserID = invokedUser.ID,
                ActionType = Enums.ActionType.DELETE_BOOK.ToString(),
                Details = $"Admin \"{invokedUser.Username}\" deleted book with ID \"{bookID}\"",
                isDeleted = false
            });

            return NoContent();
        }
    }
}
