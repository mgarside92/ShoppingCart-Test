using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Services.Interfaces;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var result = _cartService.GetCart();
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors.Select(e => e.Message));
        }

        [HttpPost("AddToCart/{id}")]
        public IActionResult AddToCart(Guid id)
        {
            var result = _cartService.AddToCart(id);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors.Select(e => e.Message));
        }

        [HttpPost("RemoveFromCart/{id}")]
        public IActionResult RemoveFromCart(Guid id)
        {
            var result = _cartService.RemoveFromCart(id);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors.Select(e => e.Message));
        }
    }
}
