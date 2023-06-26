namespace ShoppingCart.Repositories.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        IProductRepository ProductRepository { get; }
        IDealRepository DealRepository { get; }
        ICartItemRepository CartItemRepository { get; }
    }
}