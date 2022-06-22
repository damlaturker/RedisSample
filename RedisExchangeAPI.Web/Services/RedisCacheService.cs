using StackExchange.Redis;
using Newtonsoft.Json;

namespace RedisExchangeAPI.Web.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly IConfiguration _configuration;

        public RedisCacheService(IConnectionMultiplexer connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }

        public string CreateCacheKey<TKey>(object keyObject)
        {
            string cacheKey = "";
            var data = typeof(TKey);
            cacheKey += data.Name;
            var properties = data.GetProperties();
            foreach (var property in properties)
            {
                cacheKey += "." + property.Name.ToString();
                cacheKey += "=" + property.GetValue(keyObject) + "/";
            }
            return cacheKey;
        }

        public async Task<T> GetCacheValueAsync<T>(string key)
        {
            var db = _connection.GetDatabase();
            var value = await db.StringGetAsync(key);
            if (value.HasValue)
                return JsonConvert.DeserializeObject<T>(value);
            return default(T);
        }

        public async Task<bool> SetCacheValueAsync(string key, object value, TimeSpan expireTime)
        {
            var db = _connection.GetDatabase();
            return await db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiry: expireTime);
        }
        public async Task<bool> DeleteCacheValueAsync(string key)
        {
            var db = _connection.GetDatabase();
            return await db.KeyDeleteAsync(key);

        }
    }
}
