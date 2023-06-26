using FluentResults;
using ShoppingCart.Domain.Services.Interfaces;
using ShoppingCart.Models.Entities;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Domain.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository) : base(unitOfWork)
        {
            _productRepository = productRepository;
        }

        public Result<List<Product>> GetProducts()
        {
            try
            {
                var products = _productRepository.GetAll().ToList();

                return Result.Ok(products);
            }
            catch (Exception ex)
            {
                return Result.Fail("Unable to get all products").WithError(ex.Message);
            }
            
        }

		public Product GetProduct(Guid id)
		{
			var product = _productRepository.GetById(id);
			if (product == null)
				throw new Exception("Product does not exist.");

			return product;
		}

		public void SeedProducts()
        {
            List<Product> testProducts = new()
            {
                new Product
                {
                    Name = "A",
                    Price = 5
                },
                new Product
                {
                    Name = "B",
                    Price = 3
                },
                new Product
                {
                    Name = "C",
                    Price = 2
                },
                new Product
                {
                    Name = "D",
                    Price = 1.5
                },
            };

            foreach (var product in testProducts)
            {
                var exists = _productRepository.GetAll().Any(p => p.Name.ToUpper() == product.Name.ToUpper());
                if (!exists)
                {
                    _productRepository.Add(product);
                }
            }

            _unitOfWork.SaveChanges();
        }
    }
}
