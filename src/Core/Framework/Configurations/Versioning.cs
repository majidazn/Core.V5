using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configurations
{
    public static class Versioning
    {
        public static void AddApiVersioning(this IServiceCollection services, int major = 1, int minor = 0)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(major, minor);
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });
        }
    }
}