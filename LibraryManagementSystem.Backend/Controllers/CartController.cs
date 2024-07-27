using LibraryManagementSystem.Backend.DTOs;
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
        public async Task<ActionResult<Cart>> AddToCart([FromBody] CartDTO cartDTO)
        {
            try
            {
                Cart cart = await this._cartService.AddBookToCart(cartDTO.userID!.Value, cartDTO.bookID!.Value);
                return Ok(cart);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveFromCart")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Cart>> RemoveFromCart([FromBody] CartDTO cartDTO)
        {
            try
            {
                Cart cart = await this._cartService.RemoveBookFromCart(cartDTO.userID!.Value, cartDTO.bookID!.Value);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ClearCart")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Cart>> ClearCart([FromBody] CartDTO cartDTO)
        {
            try
            {
                Cart cart = await this._cartService.ClearCart(cartDTO.userID!.Value);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateCartBookQuantity")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Cart>> UpdateCartBookQuantity([FromBody] CartDTO cartDto)
        {
            if (cartDto.userID == null || cartDto.bookID == null || cartDto.quantity == null)
                return BadRequest("Invalid data.");

            var cart = await this._cartService.UpdateCartBookQuantityAsync(cartDto.userID.Value, cartDto.bookID.Value, cartDto.quantity.Value);
            
            if (cart == null)
                return NotFound("Cart or book not found.");

            return Ok(cart);
        }

        [HttpGet("{userID}")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<Cart>> GetCart(int userID)
        {
            try
            {
                var cart = await this._cartService.GetOrCreateCart(userID);
                return Ok(cart);
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
