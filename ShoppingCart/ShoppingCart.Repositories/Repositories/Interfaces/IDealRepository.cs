using ShoppingCart.Models.Entities;

namespace ShoppingCart.Repositories.Repositories.Interfaces
{
    public interface IDealRepository : IBaseRepository<Deal>
    {
		Deal GetDealByProductId(Guid productId);

	}
}
