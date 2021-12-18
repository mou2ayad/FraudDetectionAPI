using System;
using System.Collections.Generic;
using Enyim.Caching.Configuration;
using FRISS.Components.Utilities.APIKey;
using FRISS.Components.Utilities.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FRISS.Components.Utilities.DependencyInjection
{
    public static class CacheInjectionExtension
    {
        public static void InjectCacheService(this IServiceCollection services,IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("IsLocalEnv"))
                services.AddSingleton<ICache, InMemoryCache>();
            else
                services.AddSharedCache(configuration);

        }
        private static void AddSharedCache(this IServiceCollection container, IConfiguration configuration)
        {
            container.AddTransient<ICache, DistributedCache>();
            container.AddEnyimMemcached(o => o.Servers = new List<Server>
            {
                new Server
                {
                    Address = configuration["DistributedCache:Address"],
                    Port = Convert.ToInt32(configuration["DistributedCache:Port"])
                }
            });
        }
    }

}
