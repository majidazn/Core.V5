using Framework.Exceptions;
using Framework.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Framework.TenantConfiguration
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int? GetTenantId()
        {
            try
            {
                if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                    return 0;

                if (_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == "TenantId"))
                {
                    var tenantId = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "TenantId").FirstOrDefault().Value;

                    if (!string.IsNullOrEmpty(tenantId))
                        return tenantId.ToInteger(0);
                }
                else if (_httpContextAccessor.HttpContext.Request.Cookies["TenantId"] != null)
                {
                    var tenantIdInCookie = _httpContextAccessor.HttpContext.Request.Cookies["TenantId"];

                    return tenantIdInCookie.ToInteger(0);
                }
                else if (_httpContextAccessor.HttpContext.Request.Headers.Any(x => x.Key == "TenantId"))
                {
                    var tenant = _httpContextAccessor.HttpContext.Request.Headers["TenantId"].ToString();
                    return tenant.ToInteger(0);
                }

                return 0;
            }
            catch (Exception)
            {
                throw new AppException();
            }
        }
    }
}