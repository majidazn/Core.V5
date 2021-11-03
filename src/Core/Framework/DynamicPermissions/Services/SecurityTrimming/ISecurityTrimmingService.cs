using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Framework.DynamicPermissions.Services.SecurityTrimming
{ 
    public interface ISecurityTrimmingService
    {
        Task<bool> CanCurrentUserAccess(string area, string controller, string action);
        Task<bool> CanUserAccess(ClaimsPrincipal user, string area, string controller, string action);
    }
}
