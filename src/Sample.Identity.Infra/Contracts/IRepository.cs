using System.Linq.Expressions;

namespace Sample.Identity.Infra.Contracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        public IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");

        public Task<TEntity> GetById(object id);

        public Task<IEnumerable<TEntity>> GetAll();

        public void Insert(TEntity obj);

        public void Update(TEntity obj);

        public void Delete(string id);
    }
}