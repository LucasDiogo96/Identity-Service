namespace Sample.Identity.Infra.Contracts
{
    public interface ICacheManager
    {
        public bool Exists(string key);

        public T Get<T>(string key);

        public void Add<T>(string key, T data, TimeSpan expiration = default);

        public void Remove(string key);
    }
}