using ShoppingCart.Models.Entities;

namespace ShoppingCart.Domain.Services.Interfaces
{
    public interface IDealService
    {
        double CalculateBalanceAndApplyDeals(List<CartItem> cartItems);
		void SeedDeals();
    }
}