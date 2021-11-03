using System.Collections.Generic;

namespace Core.Common.ViewModels.Account
{
    public class LoginResult
    {
        public string Token { get; set; }
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public string RefreshToken { get; set; }
        public string DynamicPermissionKey { get; set; }
        public List<int> StabelDynamicPermissions { get; set; }
        public List<KeyValuePair<string,string>> Claims { get; set; }
    }
}
