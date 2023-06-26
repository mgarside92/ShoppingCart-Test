using FluentResults;
using ShoppingCart.Domain.Services.Interfaces;
using ShoppingCart.Models.Entities;
using ShoppingCart.Models.ViewModels;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Domain.Services
{
    public class CartService : BaseService, ICartService
    {
        private readonly IProductService _productService;
        private readonly ICartItemRepository _cartItemRepository;
		private readonly IDealService _dealService;

		public CartService(IProductService productService, ICartItemRepository cartItemRepository, IDealService dealService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _productService = productService;
            _cartItemRepository = cartItemRepository;
			_dealService = dealService;
		}

        public Result<CartViewModel> AddToCart(Guid id)
        {
            try
            {
                var product = _productService.GetProduct(id);

                AddProductToCart(id);

				var cart = GetCartContents();

                return Result.Ok(cart);
            }
            catch (Exception ex)
            {
                return Result.Fail("Unable to add to cart").WithError(ex.Message);
            }
        }

        public Result<CartViewModel> RemoveFromCart(Guid id)
        {
            try
            {
                var product = _productService.GetProduct(id);

				RemoveProductFromCart(id);

                var cart = GetCartContents();

                return Result.Ok(cart);
            }
            catch (Exception ex)
            {
                return Result.Fail("Unable to remove from cart").WithError(ex.Message);
            }
        }

        public Result<CartViewModel> GetCart()
        {
            try
            {
                var cart = GetCartContents();
                return Result.Ok(cart);
            }
            catch (Exception ex)
            {
                return Result.Fail("Unable to get cart").WithError(ex.Message);
            }
        }

        public void AddProductToCart(Guid id)
        {
			var existingCartItem = _cartItemRepository.GetCartItemByProductId(id);
			if (existingCartItem == null)
			{
				CartItem cartItem = new()
				{
					ProductId = id,
					Quantity = 1
				};

				_cartItemRepository.Add(cartItem);
			}
			else
			{
				existingCartItem.Quantity++;
				_cartItemRepository.Update(existingCartItem);
			}

			_unitOfWork.SaveChanges();
		}

        public void RemoveProductFromCart(Guid id)
        {
			var existingCartItem = _cartItemRepository.GetCartItemByProductId(id);
			if (existingCartItem == null)
				throw new Exception("Item is not in cart");

			if (existingCartItem.Quantity == 1)
				_cartItemRepository.Delete(existingCartItem);

			if (existingCartItem.Quantity > 1)
			{
				existingCartItem.Quantity--;
				_cartItemRepository.Update(existingCartItem);
			}

			_unitOfWork.SaveChanges();
		}

        public CartViewModel GetCartContents()
        {
            var cartItems = _cartItemRepository.GetAll();

            var balance = _dealService.CalculateBalanceAndApplyDeals(cartItems.ToList());

			List<CartItemViewModel> products = cartItems.Select(c => new CartItemViewModel { ProductId = c.ProductId, ProductName = c.Product.Name, ProductPrice = c.Product.Price, ProductQuantity = c.Quantity }).ToList();

            CartViewModel cartViewModel = new()
            {
                Balance = balance,
                Products = products
            };

            return cartViewModel;
        }
    }
}
