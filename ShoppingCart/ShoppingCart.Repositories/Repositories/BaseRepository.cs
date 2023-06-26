using ShoppingCart.Models.Entities.Interfaces;
using ShoppingCart.Repositories.DAL;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Repositories.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        public readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> queryable = _dbContext.Set<TEntity>().AsQueryable();
            return queryable;
        }

        public TEntity GetById(Guid id)
        {
            TEntity entity = GetAll().SingleOrDefault(e => e.Id == id);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return entity;
        }

    }
}
