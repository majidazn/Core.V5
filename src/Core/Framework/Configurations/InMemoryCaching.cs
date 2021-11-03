using Framework.Caching;
using Framework.Caching.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configurations
{
    public static class InMemoryCachingSetup
    {
        public static void AddInMemoryCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<IMemoryCachingServices, InMemoryCaching>();
        }

        public static void AddInMemoryCachingSingleton(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IMemoryCachingServices, InMemoryCachingWithoutAwaiter>();
        }
    }
}
