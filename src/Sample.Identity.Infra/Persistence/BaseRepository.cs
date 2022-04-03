using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Sample.Identity.Infra.Contracts;
using ServiceStack;

namespace Sample.Identity.Infra.Persistence
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoContext context;
        protected IMongoCollection<TEntity> dbSet;

        public BaseRepository(IMongoContext context)
        {
            this.context = context;

            dbSet = this.context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IMongoQueryable<TEntity>? query = dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string? includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                IQueryable<TEntity>? linqQuery = query.Include(includeProperty);

                query = ((IMongoQueryable<TEntity>)linqQuery);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.AsNoTracking().ToList();
            }
        }

        public virtual async Task<TEntity> GetById(object id)
        {
            IAsyncCursor<TEntity>? data = await dbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse((string)id)));
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            IAsyncCursor<TEntity>? all = await dbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual void Insert(TEntity obj)
        {
            context.AddCommand(() => dbSet.InsertOneAsync(obj));
        }

        public virtual void Update(TEntity obj)
        {
            context.AddCommand(() => dbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse((string)obj.GetId())), obj));
        }

        public virtual void Delete(string id)
        {
            context.AddCommand(() => dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}