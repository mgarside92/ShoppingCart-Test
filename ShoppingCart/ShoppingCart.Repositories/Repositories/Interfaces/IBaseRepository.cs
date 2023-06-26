using ShoppingCart.Models.Entities.Interfaces;

namespace ShoppingCart.Repositories.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
        TEntity GetById(Guid id);
        TEntity Update(TEntity entity);
	}
}