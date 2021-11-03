using Framework.DynamicPermissions.Services.HashingService;
using Framework.DynamicPermissions.Services.SecurityTrimming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.DynamicPermissions
{
    public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DynamicPermissionsAuthorizationHandler(ISecurityTrimmingService securityTrimmingService, IHttpContextAccessor httpContextAccessor)
        {
           _securityTrimmingService = securityTrimmingService;
            this._httpContextAccessor = httpContextAccessor;
            if (_securityTrimmingService == null)
                throw new UnauthorizedAccessException($@"{nameof(_securityTrimmingService)} is null");
        }

        protected override async Task HandleRequirementAsync(
             AuthorizationHandlerContext context,
             DynamicPermissionRequirement requirement)
        {
            var routeData = _httpContextAccessor.HttpContext.GetRouteData();

            var areaName = routeData?.Values["area"]?.ToString();
            var area = string.IsNullOrWhiteSpace(areaName) ? string.Empty : areaName;

            var controllerName = routeData?.Values["controller"]?.ToString();
            var controller = string.IsNullOrWhiteSpace(controllerName) ? string.Empty : controllerName;

            var actionName = routeData?.Values["action"]?.ToString();
            var action = string.IsNullOrWhiteSpace(actionName) ? string.Empty : actionName;


            if (await _securityTrimmingService.CanCurrentUserAccess(area, controller, action))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            
        }
    }

    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
    }
}
