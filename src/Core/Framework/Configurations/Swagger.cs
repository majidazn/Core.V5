using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;

namespace Framework.Configurations
{
    public static class Swagger
    {
        public static void SwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(option =>
            {
                option.IgnoreObsoleteActions();
                option.IgnoreObsoleteProperties();
                option.SchemaGeneratorOptions.IgnoreObsoleteProperties = true;

                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Epd",
                    Description = "Etela Resani Peyvand Dadeha",
                    Contact = new OpenApiContact { Name = "EPD", Email = "info@epd.ir", Url = new Uri("https://epr.ir") },
                    License = new OpenApiLicense() { Name = "EPD", Url = new Uri("https://epr.ir") }
                });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void UseSwaggerCustom(this IApplicationBuilder app,
            string title = "EPD Project", string endpoint = "/swagger/v1/swagger.json")
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(endpoint, title);
                c.DocExpansion(DocExpansion.List);
               
            });
        }
    }
}
