using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models.Entities;
using ShoppingCart.Repositories.DAL;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Repositories.Repositories
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<CartItem> GetAllCartItem()
        {
            return _dbContext.Cart.Include(p => p.Product);
        }

        public CartItem GetCartById(Guid id)
        {
            var cartItem = GetAllCartItem().SingleOrDefault(p => p.Id == id);
            return cartItem;
		}

        public CartItem GetCartItemByProductId(Guid productId)
        {
            var cartItem = GetAllCartItem().SingleOrDefault(c => c.ProductId == productId);
            return cartItem;
        }
    }
}
