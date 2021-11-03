using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configurations
{
    public static class CORS
    {
        public static void CorsSetup(this IServiceCollection services,
          string corsName, string[] origins = null, string[] methods = null,
          string[] headers = null, string[] exposedHeaders = null)
        {
            var corsBuilder = new CorsPolicyBuilder();
           
            if (origins != null)
                corsBuilder.WithOrigins(origins)
                     .SetIsOriginAllowed((host) => true)
                     .AllowCredentials();
            else
                corsBuilder.AllowAnyOrigin();

            if (methods != null)
                corsBuilder.WithMethods(methods);
            else
                corsBuilder.AllowAnyMethod();

            if (headers != null)
                corsBuilder.WithHeaders(headers);
            else
                corsBuilder.AllowAnyHeader();

            if (exposedHeaders != null && exposedHeaders.Length > 0)
                corsBuilder.WithExposedHeaders(exposedHeaders);

            services.AddCors(options =>
            {
                options.AddPolicy(corsName, corsBuilder.Build());
            });
        }
    }
}
