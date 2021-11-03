using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Framework.RemoteData
{
    public class RemoteLoginData
    {
        public RemoteLoginData()
        {
            NewClaims = new List<KeyValuePair<string, string>>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public ClaimsPrincipal LoggedUser { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public string DynamicPermissionKey { get; set; }
        public IEnumerable<int> StableDynamicPermissions { get; set; }
        public List<KeyValuePair<string, string>> NewClaims { get; set; }
        
        

    }
}
