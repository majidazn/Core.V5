using Core.Common.Enums;
using Framework.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Configurations
{
    public static class Identity
    {
        public static void IdentitySetup(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(cfg =>
               {
                   var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstantAthenticationData.TokenKey));
                   cfg.RequireHttpsMetadata = false;
                   cfg.SaveToken = true;

                   cfg.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = ConstantAthenticationData.ValidIssuer,
                       ValidAudience = ConstantAthenticationData.ValidAudience,

                       IssuerSigningKey = tokenKey,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ClockSkew = TimeSpan.Zero
                       //   TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Encryption.PasswordKey))
                   };

                   cfg.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                           logger.LogError("Authentication failed.", context.Exception);

                           return Task.CompletedTask;
                       },
                       OnTokenValidated = context =>
                       {
                           var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                           return tokenValidatorService.ValidateAsync(context);
                       },
                       OnMessageReceived = context =>
                       {
                           // If the request is for our hub...
                           var accessToken = context?.Request?.Query["access_token"] ?? string.Empty;
                           var path = context?.HttpContext?.Request?.Path ?? string.Empty;

                           if (!string.IsNullOrEmpty(accessToken) &&
                               !string.IsNullOrEmpty(path) &&
                               (path.StartsWithSegments("/hubs")))
                           {
                               // Read the token out of the query string
                               context.Token = accessToken;
                           }

                           return Task.CompletedTask;
                       },
                       OnChallenge = context =>
                       {
                           var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                           logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                           return Task.CompletedTask;
                       }
                   };
               });

            //services.Configure<IdentityOptions>(options => {
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers= true;
            //});

            services.AddScoped<ITokenValidatorService, TokenValidatorService>();
        }

        public static void IdentityEncryptionSetup(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(cfg =>
               {
                   var tokenKey = Encoding.UTF8.GetBytes(ConstantAthenticationData.TokenKey);
                   var encryptionKey = Encoding.UTF8.GetBytes(ConstantAthenticationData.TokenEncryptionKey);

                   cfg.SaveToken = true;
                   cfg.RequireHttpsMetadata = false;
                   cfg.TokenValidationParameters = new TokenValidationParameters
                   {
                       ClockSkew = TimeSpan.Zero, // default: 5 min
                       RequireSignedTokens = true,

                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(tokenKey),

                       RequireExpirationTime = true,
                       ValidateLifetime = true,

                       ValidateAudience = true, //default : false
                       ValidAudience = ConstantAthenticationData.ValidAudience,

                       ValidateIssuer = true, //default : false
                       ValidIssuer = ConstantAthenticationData.ValidIssuer,

                       TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                   };

                   cfg.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                           logger.LogError("Authentication failed.", context.Exception);
                           return Task.CompletedTask;
                       },
                       OnTokenValidated = context =>
                       {
                           var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                           return tokenValidatorService.ValidateAsync(context);
                       },
                       OnMessageReceived = context =>
                       {
                           // If the request is for our hub...
                           var accessToken = context?.Request?.Query["access_token"] ?? string.Empty;
                           var path = context?.HttpContext?.Request?.Path ?? string.Empty;

                           if (!string.IsNullOrEmpty(accessToken) &&
                               !string.IsNullOrEmpty(path) &&
                               (path.StartsWithSegments("/hubs")))
                           {
                               // Read the token out of the query string
                               context.Token = accessToken;
                           }

                           return Task.CompletedTask;
                       },
                       OnChallenge = context =>
                       {
                           var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                           logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                           return Task.CompletedTask;
                       }
                   };
               });

            services.AddScoped<ITokenValidatorService, TokenValidatorService>();
        }
    }
}
