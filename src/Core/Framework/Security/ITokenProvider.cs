using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Security
{
    public interface ITokenProvider
    {
        string GetToken(bool hasBearer = true);
        bool HasTokenInAuthorizationHeader();
        bool HasTokenInCookie();
        bool HasTokenInQueryString();
    }
}
