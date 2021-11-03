using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace Framework.Extensions
{
    public static class IdentityExtensions
    {

        public static string GetUserId(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);
        }

        public static string GetUserTenantId(this IIdentity identity)
        {
            return identity?.GetUserClaimValue("TenantId");
        }

        public static string GetUserClaimValue(this IIdentity identity, string claimType)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity?.FindFirstValue(claimType);
        }

        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            return identity?.FindFirst(claimType)?.Value;
        }


        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            var userId = identity?.GetUserId();
            return userId.HasValue()
                ? (T)Convert.ChangeType(userId, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public static string GetUserName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.Name);
        }

        public static string GetUserFirstName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.GivenName);
        }


        public static string GetUserLastName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.Surname);
        }

        public static string GetUserFullName(this IIdentity identity)
        {
            return $"{GetUserFirstName(identity)} {GetUserLastName(identity)}";
        }
        public static string GetUserPersonId(this IIdentity identity)
        {
            return identity?.GetUserClaimValue("PersonId");
        }

    }
}
