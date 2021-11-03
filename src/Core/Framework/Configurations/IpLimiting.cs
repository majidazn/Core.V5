using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static Framework.Configurations.IpLimiting;

namespace Framework.Configurations
{
    public static class IpLimiting
    {
        public static void IpLimitingSetup(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddOptions();

            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

        public class IpRateLimitMiddlewareCustom : RateLimitMiddleware<IpRateLimitProcessor>
        {
            private readonly ILogger<IpRateLimitMiddleware> _logger;

            public IpRateLimitMiddlewareCustom(RequestDelegate next,
                //IProcessingStrategy processingStrategy,
                IOptions<IpRateLimitOptions> options,
                IRateLimitCounterStore counterStore,
                IIpPolicyStore policyStore,
                IRateLimitConfiguration config,

                ILogger<IpRateLimitMiddleware> logger)
            : base(next, options?.Value, new IpRateLimitProcessor(options?.Value, counterStore, policyStore, config), config)
            {
                _logger = logger;
            }

            protected override void LogBlockedRequest(HttpContext httpContext, ClientRequestIdentity identity, RateLimitCounter counter, RateLimitRule rule)
            {
                _logger.LogWarning($"Request {identity.HttpVerb}:{identity.Path} from IP {identity.ClientIp} has been blocked, quota {rule.Limit}/{rule.Period} exceeded by {counter.Count}. Blocked by rule {rule.Endpoint}, TraceIdentifier {httpContext.TraceIdentifier}.");
            }
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseIpRateLimitingCustom(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpRateLimitMiddlewareCustom>();
        }
    }
}
