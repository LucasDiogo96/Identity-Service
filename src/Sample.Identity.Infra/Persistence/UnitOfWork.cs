using Sample.Identity.Domain.Entities;
using Sample.Identity.Infra.Contexts;
using Sample.Identity.Infra.Contracts;

namespace Sample.Identity.Infra.Persistence
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly PersistenceContext context;

        public IRepository<User> UserRepository { get; private set; }

        public UnitOfWork(PersistenceContext context)
        {
            this.context = context;

            this.UserRepository = new BaseRepository<User>(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}