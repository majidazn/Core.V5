using Core.ServiceBus.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.ServiceBus.LocalIntegration
{
    public class BackgroundTaskDbContext : DbContext
    {
        public BackgroundTaskDbContext(DbContextOptions<BackgroundTaskDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<LocalIntegrationEvent> LocalIntegrationEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LocalIntegrationEventConfiguration());
        }
    }

    //public class BackgroundTaskDbContextFactory : IDesignTimeDbContextFactory<BackgroundTaskDbContext>
    //{
    //    public BackgroundTaskDbContext CreateDbContext(string[] args)
    //    {
    //        IConfiguration config = new ConfigurationBuilder()
    //            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
    //            .AddJsonFile("LocalIntegration/ServiceBusAppsettings.json")
    //            .Build();

    //        var optionsBuilder = new DbContextOptionsBuilder<BackgroundTaskDbContext>();
    //        var connectionString = config.GetConnectionString(nameof(BackgroundTaskDbContext));

    //        optionsBuilder.UseSqlServer(
    //            config.GetConnectionString(nameof(BackgroundTaskDbContext)),
    //            sqlServerOptionsAction: sqlOptions =>
    //            {
    //                sqlOptions.EnableRetryOnFailure(
    //                maxRetryCount: 5,
    //                maxRetryDelay: TimeSpan.FromSeconds(30),
    //                errorNumbersToAdd: null);
    //                sqlOptions.MigrationsAssembly("Core.ServiceBus");
    //            });


    //        return new BackgroundTaskDbContext(optionsBuilder.Options);
    //    }

    //}

    public class BackgroundTaskDbContextFactory : IDesignTimeDbContextFactory<BackgroundTaskDbContext>
    {
        public BackgroundTaskDbContext CreateDbContext(string[] args)
        {


            var optionsBuilder = new DbContextOptionsBuilder<BackgroundTaskDbContext>();

            optionsBuilder.UseSqlServer(ConnectionStringProvider.GetLocalIntegrationConnectionString(),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(20),
                    errorNumbersToAdd: null);
                    sqlOptions.MigrationsAssembly("Core.ServiceBus");
                });


            return new BackgroundTaskDbContext(optionsBuilder.Options);
        }

    }
}
