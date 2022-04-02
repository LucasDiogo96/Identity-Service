using System.Linq.Expressions;

namespace Sample.Identity.Infra.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");

        public TEntity GetById(object id);

        public void Insert(TEntity entity);

        public void Delete(object id);

        public void Delete(TEntity entity);

        public void Update(TEntity entity);
    }
}