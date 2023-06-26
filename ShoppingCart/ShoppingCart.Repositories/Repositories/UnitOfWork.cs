using ShoppingCart.Repositories.DAL;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private IProductRepository productRepository;
        private IDealRepository dealRepository;
        private ICartItemRepository cartItemRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(_dbContext);
                }
                return productRepository;
            }
        }

        public IDealRepository DealRepository
        {
            get
            {
                if (dealRepository == null)
                {
                    dealRepository = new DealRepository(_dbContext);
                }
                return dealRepository;
            }
        }

        public ICartItemRepository CartItemRepository
        {
            get
            {
                if (cartItemRepository == null)
                {
                    cartItemRepository = new CartItemRepository(_dbContext);
                }
                return cartItemRepository;
            }
        }
    }
}
