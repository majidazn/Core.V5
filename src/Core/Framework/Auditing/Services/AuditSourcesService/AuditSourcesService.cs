using Core.Common.Enums;
using Framework.Api;
using Framework.Extensions;
using Framework.Auditing.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Reflection;
using System.Security.Claims;

namespace Framework.Auditing.Services.AuditSourcesService
{
    public class AuditSourcesService : IAuditSourcesService
    {
        #region fields

        protected readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region ctor

        public AuditSourcesService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region methods

        public AuditNetworkDto GetAuditNetworkValues(Applications applicationId)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            return new AuditNetworkDto
            {
                ApplicationId = applicationId,
                ApplicationName = applicationId.ToDisplay(),
                TenantId = GeTenantId(),
                OperatorId = GetOperatorId(),
                OperatorName = GetOperatorNameName(httpContext),
                OperatorDisplayName = GetOperatorDisplayName(httpContext),
                HostName = GetHostName(httpContext),
                MachineName = GetComputerName(),
                LocalIpAddress = GetLocalIpAddress(httpContext),
                RemoteIpAddress = GetRemoteIpAddress(httpContext),
                UserAgent = GetUserAgent(httpContext),
                ApplicationAssemblyName = GetApplicationName(),
                ApplicationVersion = GetApplicationVersion(),
                HttpMethod = GetHttpMethod(httpContext),
                Url = UrlBuilder.BuildUrl(httpContext)
            };
        }

        public int GeTenantId()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity;
            return int.Parse(claimsIdentity?.FindFirst("TenantId")?.Value);
        }

        public int GetOperatorId()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            return int.Parse(claimsIdentity?.FindFirst("PersonId")?.Value);
        }

        private string GetOperatorNameName(HttpContext httpContext)
        {
            return httpContext.User.Identity.Name;
        }

        private string GetOperatorDisplayName(HttpContext httpContext)
        {
            var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            return claimsIdentity?.FindFirst("UserDisplayName")?.Value;
        }

        private string GetHttpMethod(HttpContext httpContext)
            => httpContext?.Request?.Method;

        private string GetUserAgent(HttpContext httpContext)
            => httpContext.Request?.Headers["User-Agent"].ToString();

        private string GetRemoteIpAddress(HttpContext httpContext)
            => httpContext.Connection?.RemoteIpAddress?.ToString();

        private string GetLocalIpAddress(HttpContext httpContext)
             => httpContext.Connection?.LocalIpAddress?.ToString();

        private string GetHostName(HttpContext httpContext)
            => httpContext.Request.Host.ToString();


        private string GetComputerName()
            => Environment.MachineName;

        private string GetApplicationName()
            => Assembly.GetEntryAssembly()?.GetName().Name;


        private string GetApplicationVersion()
            => Assembly.GetEntryAssembly()?.GetName().Version.ToString();


        private string GetClientVersion(HttpContext httpContext)
            => httpContext.Request?.Headers["client-version"];

        private string GetClientName(HttpContext httpContext)
            => httpContext.Request?.Headers["client-name"];

        #endregion

    }
}
