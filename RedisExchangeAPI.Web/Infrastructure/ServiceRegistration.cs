using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration configuration)
        {
            #region Cache
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<IConnectionMultiplexer>(
                opt =>
                    ConnectionMultiplexer.Connect(configuration.GetSection("Redis")["Host"] + ":" + configuration.GetSection("Redis")["Port"]+ ",abortConnect=false")
            );
            #endregion
        }
    }
}
