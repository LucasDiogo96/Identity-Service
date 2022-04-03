using MongoDB.Driver;

namespace Sample.Identity.Infra.Contracts
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);

        Task<int> SaveChanges();

        IMongoCollection<T> GetCollection<T>(string name);
    }
}