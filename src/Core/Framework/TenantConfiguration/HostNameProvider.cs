using Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.TenantConfiguration
{
    public class HostNameProvider : IHostNameProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public HostNameProvider(IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }
        public string GetHostName()
        {
            if (httpContextAccessor == null || httpContextAccessor.HttpContext == null ||
                httpContextAccessor.HttpContext.Request == null)
                return string.Empty;

            return httpContextAccessor.HttpContext.Request.Host.Value ?? string.Empty;
        }

        public string GetReferer()
        {
            if (httpContextAccessor == null || httpContextAccessor.HttpContext == null ||
              httpContextAccessor.HttpContext.Request == null)
                return string.Empty;

            return httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString() ?? string.Empty;
        }
        public bool IsSSL()
        {
            if (configuration["IsSSL"] != null && !string.IsNullOrEmpty(configuration["IsSSL"]))
                return configuration["IsSSL"].ToBoolean(false);
            else if (httpContextAccessor == null || httpContextAccessor.HttpContext == null ||
           httpContextAccessor.HttpContext.Request == null)
                return false;

            return httpContextAccessor.HttpContext.Request.Scheme.ToLower().Contains("https");
        }
    }
}
