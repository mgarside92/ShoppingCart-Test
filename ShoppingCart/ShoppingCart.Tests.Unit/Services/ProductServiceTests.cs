using Moq;
using ShoppingCart.Domain.Services;
using ShoppingCart.Models.Entities;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Tests.Unit.Services
{
	[TestFixture]
	public class ProductServiceTests
	{
		private Mock<IProductRepository> _mockProductRepository;
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private ProductService _productService;

		[SetUp]
		public void Setup()
		{
			_mockProductRepository = new Mock<IProductRepository>();
			_mockUnitOfWork = new Mock<IUnitOfWork>();

			_productService = new ProductService(_mockUnitOfWork.Object, _mockProductRepository.Object);
		}

		[Test]
		public void GetProduct_ExistingProduct_ShouldReturnProduct()
		{
			// Arrange
			var productId = Guid.NewGuid();
			var product = new Product { Id = productId, Name = "A", Price = 5 };

			_mockProductRepository.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(product);

			// Act
			var result = _productService.GetProduct(productId);

			// Assert
			Assert.IsNotNull(result);
			Assert.That(result.Id, Is.EqualTo(product.Id));
			Assert.That(result.Name, Is.EqualTo(product.Name));
			Assert.That(result.Price, Is.EqualTo(product.Price));
		}

		[Test]
		public void GetProduct_NonExistingProduct_ShouldThrowException()
		{
			// Arrange
			var productId = Guid.NewGuid();

			_mockProductRepository.Setup(m => m.GetById(It.IsAny<Guid>())).Returns((Product)null);

			// Act
			var ex = Assert.Throws<Exception>(() => _productService.GetProduct(productId));

			// Assert
			Assert.That(ex.Message, Is.EqualTo("Product does not exist."));
		}
	}
}
