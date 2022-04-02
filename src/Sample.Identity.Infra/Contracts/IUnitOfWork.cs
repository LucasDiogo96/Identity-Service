using Sample.Identity.Domain.Entities;

namespace Sample.Identity.Infra.Contracts
{
    public interface IUnitOfWork
    {
        public void Save();

        public void Dispose();

        IRepository<User> UserRepository { get; }
    }
}