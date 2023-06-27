using Moq;
using ShoppingCart.Domain.Services;
using ShoppingCart.Models.Entities;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Tests.Unit.Services
{
	public class DealServiceTests
	{
		private Mock<IProductRepository> _mockProductRepository;
		private Mock<IDealRepository> _mockDealRepository;
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private DealService _dealService;

		[SetUp]
		public void Setup()
		{
			_mockProductRepository = new Mock<IProductRepository>();
			_mockDealRepository = new Mock<IDealRepository>();
			_mockUnitOfWork = new Mock<IUnitOfWork>();

			_dealService = new DealService(_mockUnitOfWork.Object, _mockProductRepository.Object, _mockDealRepository.Object);
		}

		[Test]
		public void CalculateBalanceAndApplyDeals_NoDeals_ShouldReturnCorrectBalance()
		{
			// Arrange
			var cartItems = new List<CartItem>
			{
				new CartItem { ProductId = Guid.NewGuid(), Quantity = 3, Product = new Product { Price = 100 } }
			};

			_mockDealRepository.Setup(m => m.GetDealByProductId(It.IsAny<Guid>())).Returns((Deal)null);

			// Act
			double balance = _dealService.CalculateBalanceAndApplyDeals(cartItems);

			// Assert
			Assert.That(balance, Is.EqualTo(300));
		}

		[Test]
		public void CalculateBalanceAndApplyDeals_WithDeals_ShouldReturnCorrectBalance()
		{
			// Arrange
			var productId = Guid.NewGuid();
			var deal = new Deal { ProductId = productId, DealQuantity = 2, DealPrice = 150 };
			var cartItems = new List<CartItem>
			{
				new CartItem { ProductId = productId, Quantity = 3, Product = new Product { Price = 100 } }
			};

			_mockDealRepository.Setup(m => m.GetDealByProductId(It.IsAny<Guid>())).Returns(deal);

			// Act
			double balance = _dealService.CalculateBalanceAndApplyDeals(cartItems);

			// Assert
			Assert.That(balance, Is.EqualTo(250));
		}
	}
}
