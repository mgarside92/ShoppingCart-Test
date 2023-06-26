using ShoppingCart.Models.Entities;
using ShoppingCart.Repositories.DAL;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Repositories.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
