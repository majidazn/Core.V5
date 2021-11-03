using Core.ServiceBus.Extensions;
using Core.ServiceBus.LocalIntegration.Repositories;
using Core.ServiceBus.LocalIntegration.Services;
using Core.ServiceBus.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.ServiceBus.LocalIntegration
{
    public static class LocalntegrationEventServiceCollectionExtensions
    {
        public static void AddLocalntegrationEvent(this IServiceCollection services)
        {
            services.AddDbContextPool<BackgroundTaskDbContext>(options => options.UseSqlServer(
                ConnectionStringProvider.GetLocalIntegrationConnectionString(),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(20),
                    errorNumbersToAdd: null);
                }));


            services.AddScoped<DbContext, BackgroundTaskDbContext>();
            services.AddScoped<ILocalIntegrationEventRepository, LocalIntegrationEventRepository>();
            services.AddScoped<IBackgroundTaskLocalIntegrationEventRepository, BackgroundTaskLocalIntegrationEventRepository>();
            services.AddScoped<IBackgroundTaskLocalIntegrationEventService, BackgroundTaskLocalIntegrationEventService>();
            services.AddHostedService<EventPublisher>();
            services.AddHostedService<RequeueAtStartup>();
        }
    }
}
