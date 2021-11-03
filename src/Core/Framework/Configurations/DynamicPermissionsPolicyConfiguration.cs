using Framework.DynamicPermissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Framework.Configurations
{
    public static class DynamicPermissionsPolicyConfiguration
    {
        public static void AddDynamicPermissionsPolicy(this IServiceCollection services)
        {            
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(
                name: ConstantPolicies.DynamicPermission,
                configurePolicy: policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new DynamicPermissionRequirement());
                    policy.AuthenticationSchemes = new List<string> { JwtBearerDefaults.AuthenticationScheme };
                });
            });
        }
    }
}