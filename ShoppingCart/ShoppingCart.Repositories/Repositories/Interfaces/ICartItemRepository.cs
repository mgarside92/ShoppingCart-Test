using ShoppingCart.Models.Entities;

namespace ShoppingCart.Repositories.Repositories.Interfaces
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
		CartItem GetCartItemByProductId(Guid productId);
	}
}
