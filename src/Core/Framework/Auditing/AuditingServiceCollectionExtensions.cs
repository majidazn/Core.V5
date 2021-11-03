
using Framework.AuditBase.AuditBaseService;
using Framework.Auditing.Services.AuditSourcesService;
using Framework.Auditing.Services.EntityAuditService;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Auditing
{
    public static class AuditingServiceCollectionExtensions
    {
        public static void AddAuditing(this IServiceCollection services)
        {
            services.AddScoped<IEntityAuditService, EntityAuditService>();
            services.AddScoped<IAuditSourcesService, AuditSourcesService>();
            services.AddScoped<IAuditBaseService, AuditBaseService>();
        }
    }
}
