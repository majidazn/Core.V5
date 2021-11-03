    using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.LocalIntegration
{
    public static class ApplicationBuilderExtensions
    {
        public static void IntializeLocalIntegrationEventDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<BackgroundTaskDbContext>(); //Service locator

                //Applies any pending migrations for the context to the database like (Update-Database)
                dbContext.Database.Migrate();

            }
        }
    }
}
