namespace Sample.Identity.Infra.Contracts
{
    public interface ICacheContext
    {
        public Task<T> Get<T>(string key);

        public Task Add<T>(string key, T data, TimeSpan expiration = default);

        public Task Remove(string key);
    }
}