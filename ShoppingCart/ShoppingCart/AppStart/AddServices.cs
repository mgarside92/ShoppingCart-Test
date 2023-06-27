using ShoppingCart.Domain.Services;
using ShoppingCart.Domain.Services.Interfaces;
using ShoppingCart.Repositories.Repositories.Interfaces;
using ShoppingCart.Repositories.Repositories;

namespace ShoppingCart.AppStart
{
    public static class AddServices
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IDealService, DealService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<IDealRepository, DealRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
