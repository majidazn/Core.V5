using Microsoft.AspNetCore.Http;

namespace Framework.Security
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string GetToken(bool hasBearer = true)
        {
            var token = httpContextAccessor?.HttpContext?.Request?
                            .Headers["Authorization"].ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(token))
                token = httpContextAccessor?.HttpContext?.Response?
                            .Headers["Authorization"].ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(token))
                token = httpContextAccessor?.HttpContext?.Request?.Cookies["token"]?.ToString();

            if (string.IsNullOrEmpty(token))
                token = httpContextAccessor?.HttpContext?.Request?.Query["token"].ToString() ?? string.Empty;

            if (!string.IsNullOrEmpty(token)
                && !token.ToLower().Contains("bearer")
                && hasBearer)
                token = $"bearer {token}";

            else if (!string.IsNullOrEmpty(token)
              && token.ToLower().Contains("bearer")
              && !hasBearer)
                token = token.Remove(0, "bearer".Length).Trim();

            return token;
        }
        public bool HasTokenInAuthorizationHeader()
        {
            return (!string.IsNullOrEmpty(httpContextAccessor?.HttpContext?.Request?
                             .Headers["Authorization"].ToString() ?? string.Empty));
        }
        public bool HasTokenInCookie()
        {
            return (!string.IsNullOrEmpty(httpContextAccessor?.HttpContext?.Request?
                .Cookies["token"]?.ToString() ?? string.Empty));
        }
        public bool HasTokenInQueryString()
        {
            return (!string.IsNullOrEmpty(httpContextAccessor?.HttpContext?.Request?
                .Query["token"].ToString() ?? string.Empty));
        }
    }
}
