using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Services.Interfaces;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var result = _productService.GetProducts();
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors.Select(e => e.Message));
        }
    }
}
