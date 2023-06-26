using FluentResults;
using ShoppingCart.Models.Entities;

namespace ShoppingCart.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Product GetProduct(Guid id);
        Result<List<Product>> GetProducts();
        void SeedProducts();
    }
}