using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models.Entities;
using ShoppingCart.Repositories.DAL;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Repositories.Repositories
{
    public class DealRepository : BaseRepository<Deal>, IDealRepository
    {
        public DealRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Deal> GetAllDeals()
        {
            return _dbContext.Deals.Include(p => p.Product);
        }

        public Deal GetDealByProductId(Guid productId)
        {
            var deal = GetAllDeals().FirstOrDefault(d => d.ProductId == productId);
            return deal;
        }
    }
}
