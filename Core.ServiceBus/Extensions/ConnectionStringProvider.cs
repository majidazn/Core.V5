using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.ServiceBus.Extensions
{
    public static class ConnectionStringProvider
    {
        public static string GetLocalIntegrationConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            return root.GetSection("ConnectionStrings").GetSection("LocalIntegrationEventConnection").Value;
        }
    }
}
