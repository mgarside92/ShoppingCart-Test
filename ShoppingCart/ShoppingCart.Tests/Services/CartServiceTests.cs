using Moq;
using ShoppingCart.Domain.Services;
using ShoppingCart.Domain.Services.Interfaces;
using ShoppingCart.Models.Entities;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Tests.Services
{
	public class CartServiceTests
	{
		private Mock<IProductService> _mockProductService;
		private Mock<ICartItemRepository> _mockCartItemRepository;
		private Mock<IDealService> _mockDealService;
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private CartService _cartService;

		[SetUp]
		public void Setup()
		{
			_mockProductService = new Mock<IProductService>();
			_mockCartItemRepository = new Mock<ICartItemRepository>();
			_mockDealService = new Mock<IDealService>();
			_mockUnitOfWork = new Mock<IUnitOfWork>();

			_cartService = new CartService(_mockProductService.Object, _mockCartItemRepository.Object, _mockDealService.Object, _mockUnitOfWork.Object);
		}

		[Test]
		public void AddProductToCart_ItemNotInCart_ShouldAddItem()
		{
			var productId = Guid.NewGuid();

			_mockCartItemRepository.Setup(m => m.GetCartItemByProductId(productId)).Returns((CartItem)null);

			_cartService.AddProductToCart(productId);

			_mockCartItemRepository.Verify(m => m.Add(It.IsAny<CartItem>()), Times.Once);
			_mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
		}

		[Test]
		public void AddProductToCart_ItemInCart_ShouldIncreaseQuantity()
		{
			var productId = Guid.NewGuid();
			var existingCartItem = new CartItem { ProductId = productId, Quantity = 1 };

			_mockCartItemRepository.Setup(m => m.GetCartItemByProductId(productId)).Returns(existingCartItem);

			_cartService.AddProductToCart(productId);

			_mockCartItemRepository.Verify(m => m.Update(It.Is<CartItem>(c => c.Quantity == 2)), Times.Once);
			_mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
		}

		[Test]
		public void RemoveProductFromCart_ItemInCartAndQuantityMoreThanOne_ShouldDecreaseQuantity()
		{
			var productId = Guid.NewGuid();
			var existingCartItem = new CartItem { ProductId = productId, Quantity = 2 };

			_mockCartItemRepository.Setup(m => m.GetCartItemByProductId(productId)).Returns(existingCartItem);

			_cartService.RemoveProductFromCart(productId);

			_mockCartItemRepository.Verify(m => m.Update(It.Is<CartItem>(c => c.Quantity == 1)), Times.Once);
			_mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
		}

		[Test]
		public void RemoveProductFromCart_ItemInCartAndQuantityEqualsOne_ShouldRemoveItem()
		{
			var productId = Guid.NewGuid();
			var existingCartItem = new CartItem { ProductId = productId, Quantity = 1 };

			_mockCartItemRepository.Setup(m => m.GetCartItemByProductId(productId)).Returns(existingCartItem);

			_cartService.RemoveProductFromCart(productId);

			_mockCartItemRepository.Verify(m => m.Delete(existingCartItem), Times.Once);
			_mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
		}

		[Test]
		public void GetCartContents_ShouldReturnCorrectCartViewModel()
		{
			var cartItems = new List<CartItem>
			{
				new CartItem { ProductId = Guid.NewGuid(), Quantity = 1, Product = new Product { Name = "Product1", Price = 100 } },
				new CartItem { ProductId = Guid.NewGuid(), Quantity = 2, Product = new Product { Name = "Product2", Price = 200 } }
			};

			_mockCartItemRepository.Setup(m => m.GetAll()).Returns(cartItems.AsQueryable());
			_mockDealService.Setup(m => m.CalculateBalanceAndApplyDeals(It.IsAny<List<CartItem>>())).Returns(500);

			var cart = _cartService.GetCartContents();
			Assert.Multiple(() =>
			{
				Assert.That(cart.Balance, Is.EqualTo(500));
				Assert.That(cart.Products.Count, Is.EqualTo(2));
				Assert.That(cart.Products[0].ProductName, Is.EqualTo("Product1"));
				Assert.That(cart.Products[1].ProductName, Is.EqualTo("Product2"));
			});
		}
	}
}
