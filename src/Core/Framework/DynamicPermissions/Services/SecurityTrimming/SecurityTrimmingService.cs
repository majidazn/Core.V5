using Core.Common.Enums;
using Framework.DynamicPermissions.Services.HashingService;
using Framework.DynamicPermissions.Services.MvcActionsDiscovery;
using Framework.Extensions;
using Framework.RemoteData;
using Framework.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Framework.DynamicPermissions.Services.SecurityTrimming
{
    public class SecurityTrimmingService : ISecurityTrimmingService
    {
        //private static ConcurrentDictionary<int, IEnumerable<int>> CachedData = new ConcurrentDictionary<int, IEnumerable<int>>();
        private ConcurrentDictionary<int, IEnumerable<int>> CachedData = new ConcurrentDictionary<int, IEnumerable<int>>();

        const string DynamicPermissionKey = "DynamicPermissionKey";

        private readonly HttpContext _httpContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;
        private readonly IHashingService _hashingService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider tokenProvider;

        public SecurityTrimmingService(
            IHttpContextAccessor httpContextAccessor,
            IMvcActionsDiscoveryService mvcActionsDiscoveryService,
            IHashingService hashingService,
            IHttpClientFactory httpClientFactory
             , ITokenProvider tokenProvider
           )
        {
            _httpContextAccessor = httpContextAccessor;
            if (_httpContextAccessor == null)
                throw new UnauthorizedAccessException($@"{nameof(_httpContextAccessor)} is null");

            _httpContext = _httpContextAccessor.HttpContext;

            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
            if (_mvcActionsDiscoveryService == null)
                throw new UnauthorizedAccessException($@"{nameof(_mvcActionsDiscoveryService)} is null");

            _hashingService = hashingService;
            this._httpClientFactory = httpClientFactory;
            this.tokenProvider = tokenProvider;
        }

        public async Task<bool> CanCurrentUserAccess(string area, string controller, string action)
        {
            return _httpContext != null && await CanUserAccess(_httpContext.User, area, controller, action);
        }

        public async Task<bool> CanUserAccess(ClaimsPrincipal user, string area, string controller, string action)
        {
            if (user.IsInRole(ConstantRoles.Admin) ||
                user.IsInRole(ConstantRoles.EPDAdmin) ||
                user.IsInRole(ConstantRoles.CenterAdmin))
                return true;

            var currentClaimValue = $"{area}:{controller}:{action}";
            var hashedCurrentClaimValue = _hashingService.GetSha256Hash(currentClaimValue);
            var securedControllerActions = _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);

            if (!securedControllerActions.SelectMany(x => x.MvcActions).Any(x => x.ActionId == currentClaimValue))
                throw new KeyNotFoundException($@"The `secured` area={area}/controller={controller}/action={action} with `ConstantPolicies.DynamicPermission` policy not found. Please check you have entered the area/controller/action names correctly and also it's decorated with the correct security policy.");

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                return false;

            if (!string.IsNullOrEmpty(controller) || !string.IsNullOrEmpty(action))
            {
                var intHashCode = hashedCurrentClaimValue.GetStableHashCode();

                var dynamicPermissionKey = CheckIfDynamicPermissionKeyIsReady(user.Claims);

                //if (!CachedData.TryGetValue(dynamicPermissionKey.GetStableHashCode(), out IEnumerable<int> permissions))
                var permissions = await FillCache(user.Claims);

                return permissions.Contains(intHashCode);
            }

            return false;
        }

        protected string UserIPClient()
        {
            var remoteIpAddress = _httpContext.Connection.RemoteIpAddress;

            if (remoteIpAddress == null)
                remoteIpAddress = _httpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;

            return remoteIpAddress.ToString();
        }

        private string CheckIfDynamicPermissionKeyIsReady(IEnumerable<Claim> claims)
        {
            var dynamicPermissionKey = claims.FirstOrDefault(q => q.Type == DynamicPermissionKey);
            if (dynamicPermissionKey == null)
                throw new Exception("Login please");

            return dynamicPermissionKey.Value;
        }

        private async Task<IEnumerable<int>> FillCache(IEnumerable<Claim> claims)
        {
            var userId = claims.FirstOrDefault(q => q.Type == ClaimTypes.UserData);
            var data = await HttpClientFactoryRequest.Get<RemoteLoginData>.GetDataByClientNameAsync(
               _httpClientFactory, HttpClientNameType.SecurityWebApi.ToString(),
              $"/WebServices/GetUserRolesDynamicPermission",
              new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("userId", userId.Value) }
              , tokenProvider.GetToken());

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10));

            CachedData.AddOrUpdate(data.DynamicPermissionKey.GetStableHashCode(), data.StableDynamicPermissions, (key, oldValue) => data.StableDynamicPermissions);

            return data.StableDynamicPermissions;
        }
    }
}
