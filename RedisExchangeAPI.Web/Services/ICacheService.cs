namespace RedisExchangeAPI.Web.Services
{
    public interface ICacheService
    {
        string CreateCacheKey<TKey>(object keyObject);
        Task<T> GetCacheValueAsync<T>(string key);
        Task<bool> SetCacheValueAsync(string key, object value, TimeSpan expireTime);
        Task<bool> DeleteCacheValueAsync(string key);

    }
}
