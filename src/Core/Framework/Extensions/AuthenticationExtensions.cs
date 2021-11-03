using Core.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Framework.Extensions
{
    public static class AuthenticationExtensions
    {
        public static long GetUserId(this IHttpContextAccessor _httpContextAccessor)
        {

            if (_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == ClaimTypes.UserData))
            {
                var userId = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.UserData).FirstOrDefault().Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    return long.Parse(userId);
                }
            }

            return 0;
        }
        public static int GetPersonId(this IHttpContextAccessor _httpContextAccessor)
        {

            if (_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == "PersonId"))
            {
                var personId = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "PersonId").FirstOrDefault().Value;

                if (!string.IsNullOrEmpty(personId))
                {
                    return int.Parse(personId);
                }
            }

            return 0;
        }

        public static string GetClaimStringByType(this IHttpContextAccessor _httpContextAccessor, string type)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == type))
            {
                var value = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == type).FirstOrDefault().Value;
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }
            return string.Empty;
        }
        public static Claim GetClaimByType(this IHttpContextAccessor _httpContextAccessor, string type)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == type))
            {
                var value = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == type).FirstOrDefault();
                return value;
            }
            return null;
        }

        public static int GetTenantId(this IHttpContextAccessor _httpContextAccessor)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == "TenantId"))
            {
                var tenant = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "TenantId").FirstOrDefault().Value;

                if (!string.IsNullOrEmpty(tenant))
                {
                    return int.Parse(tenant);
                }
            }
            return 0;
        }
        public static List<Claim> GetRoleList(this IHttpContextAccessor _httpContextAccessor)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == "RoleId"))
            {
                var roles = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "RoleId").ToList();

                if (roles.Count > 0)
                    return roles;
            }

            return null;
        }

        public static List<int> GetTenantAccessId(this IHttpContextAccessor _httpContextAccessor)
        {
            var tenantList = new List<int>();

            if (_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == "AccessTenantIds"))
            {


                var accessTenantClaims = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "AccessTenantIds").FirstOrDefault();
                if (accessTenantClaims == null)
                    return tenantList;
                var accessTenantList = accessTenantClaims.Value.Split(';').ToList();
                var accessTenantIds = accessTenantList.Where(q => q != string.Empty).Select(s => Convert.ToInt32(s)).ToList();
                return accessTenantIds;
            }
            return tenantList;
        }
        public static int GetHighestRole(this IHttpContextAccessor _httpContextAccessor)
        {
            var userRoleList = _httpContextAccessor.GetRoleList();
            if (userRoleList.Count() == 0 || userRoleList == null)
                return 0;

            if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.Admin))
                return (int)AccountTypeRoles.Admin;

            else if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.UniAdmin))
                return (int)AccountTypeRoles.UniAdmin;

            else if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.EPDAdmin))
                return (int)AccountTypeRoles.EPDAdmin;

            else if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.CenterAdmin))
                return (int)AccountTypeRoles.CenterAdmin;

            else if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.CentersAdminResponsible))
                return (int)AccountTypeRoles.CentersAdminResponsible;

            else if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.HumanResourceResponsible))
                return (int)AccountTypeRoles.HumanResourceResponsible;

            else if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.HumanResourceResponsibleInCenter))
                return (int)AccountTypeRoles.HumanResourceResponsibleInCenter;

            else if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.EPDUser))
                return (int)AccountTypeRoles.EPDUser;

            else if (userRoleList.Any(a => Convert.ToInt32(a.Value) == (int)AccountTypeRoles.CenterUser))
                return (int)AccountTypeRoles.CenterUser;

            else
                return 0;
        }
    }
}
