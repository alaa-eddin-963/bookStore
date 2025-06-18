using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserIdFromToken();
            var result = await _cartService.GetCart(userId);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest request)
        {
            var userId = GetUserIdFromToken();
            await _cartService.AddToCart(userId, request.BookId, request.Quantity);
            return Ok();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] Guid bookId)
        {
            var userId = GetUserIdFromToken();
            await _cartService.RemoveFromCart(userId, bookId);
            return Ok();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserIdFromToken();
            await _cartService.ClearCart(userId);
            return Ok();
        }

        public class CartRequest
        {
            public Guid BookId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
