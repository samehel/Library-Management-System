using LibraryManagementSystem.Backend.Models;
using LibraryManagementSystem.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Backend.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService, ICartBookService cartBookService)
        {
            this._cartService = cartService;
        }

        [HttpPost("AddToCart")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Cart>> AddToCart(int userID, int bookID)
        {
            try
            {
                Cart cart = await this._cartService.AddBookToCart(userID, bookID);
                return Ok(cart);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveFromCart")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Cart>> RemoveFromCart(int userID, int bookID)
        {
            try
            {
                Cart cart = await this._cartService.RemoveBookFromCart(userID, bookID);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ClearCart")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Cart>> ClearCart(int userID)
        {
            try
            {
                Cart cart = await this._cartService.ClearCart(userID);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
