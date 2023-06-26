using FluentResults;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Domain.Services.Interfaces
{
    public interface ICartService
    {
        Result<CartViewModel> AddToCart(Guid id);
        Result<CartViewModel> RemoveFromCart(Guid id);
        Result<CartViewModel> GetCart();
    }
}