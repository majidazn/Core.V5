using Core.Common.Enums;
using Core.Common.ViewModels.JWT;
using Framework.Extensions;
using Framework.RemoteData;
using Framework.RemoteData.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security
{
    public interface ITokenValidatorService
    {
        Task ValidateAsync(TokenValidatedContext context);
    }

    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public TokenValidatorService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task ValidateAsync(TokenValidatedContext context)
        {
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;


            if (claimsIdentity.Claims?.Any() != true)
            {
                context.Fail("This token has no claims.");
            }
            else
            {
                var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
                var userIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
                var tenantId = claimsIdentity.FindFirst("TenantId").Value.ToInteger(0);

                if (serialNumberClaim == null)
                {
                    context.Fail("This is not our issued token. It has no serial.");
                    //return;
                }
                else if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                {
                    context.Fail("This is not our issued token. It has no user-id.");
                }
                else
                {
                    var accessToken = context.SecurityToken as JwtSecurityToken;
                    if (string.IsNullOrWhiteSpace(accessToken.RawData))
                        context.Fail("Token has been expired.");

                    if (accessToken.ValidTo < DateTimeOffset.UtcNow)
                    {
                        context.Fail("Token has been expired.");
                    }
                    else
                    {
                        var validateToken = new ValidateToken { TokenRawData = accessToken.RawData, UserId = userId, TenantId = tenantId };
                        var model = new PostClientRequestDto<ValidateToken>
                        {
                            ApiUrl = "/api/account/ValidateToken/",
                            ClientFactory = _httpClientFactory,
                            ClientName = nameof(HttpClientNameType.SecurityWebApi),
                            InputModel = validateToken,
                            Token = _tokenProvider.GetToken()
                        };

                        var result = await APIRequest.Post<ValidateToken, bool>.PostDataAsync(model);

                        if (!result.IsSucceeded || !result.Output)
                            context.Fail("Token has been expired.");
                    }
                }
            }
        }
    }
}
