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
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IDealService, DealService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IDealRepository, DealRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
