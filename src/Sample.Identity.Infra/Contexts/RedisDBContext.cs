using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Sample.Identity.Infra.Contracts;

namespace Sample.Identity.Infra.Contexts
{
    public class RedisDBContext : ICacheContext
    {
        private readonly IDistributedCache cache;
        private readonly DistributedCacheEntryOptions options;

        public RedisDBContext(IDistributedCache cache, DistributedCacheEntryOptions options)
        {
            this.cache = cache;
            this.options = options;
        }

        public async Task<T> Get<T>(string key)
        {
            byte[] data = await cache.GetAsync(key);

            if (data is not null)
            {
                string json = Encoding.UTF8.GetString(data);

                return JsonConvert.DeserializeObject<T>(json);
            }

            return default;
        }

        public async Task Add<T>(string key, T data, TimeSpan expiration = default)
        {
            // Serialize it
            string json = JsonConvert.SerializeObject(data);

            // Get bytes
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            // Add a custom expiration time
            if (expiration == default)
                options.SlidingExpiration = expiration;

            // Persist in cache
            await cache.SetAsync(key, bytes, options).ConfigureAwait(false);
        }

        public async Task Remove(string key)
        {
            await cache.RemoveAsync(key).ConfigureAwait(false);
        }
    }
}