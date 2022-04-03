using Newtonsoft.Json;
using Sample.Identity.Infra.Contracts;
using StackExchange.Redis;

namespace Sample.Identity.Infra.Contexts
{
    public class RedisDBContext : ICacheManager
    {
        private readonly IDatabase context;

        public RedisDBContext(IDatabase context)
        {
            this.context = context;
        }

        public bool Exists(string key)
        {
            return context.KeyExists(key);
        }

        public void Add<T>(string key, T data, TimeSpan expiration = default)
        {
            string json = JsonConvert.SerializeObject(data);

            if (expiration == default)
                expiration = TimeSpan.FromMinutes(15);

            context.StringSetAsync(key, json, expiration);
        }

        public T Get<T>(string key)
        {
            string data = context.StringGet(key);

            if (data is not null)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }

            return default;
        }

        public void Remove(string key)
        {
            context.KeyDelete(key);
        }
    }
}