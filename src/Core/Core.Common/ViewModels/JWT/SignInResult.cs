using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.ViewModels.JWT
{
    public class SignInResult
    {
        public string Token { get; set; }
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public string DynamicPermissionKey { get; set; }
        public List<int> StabelDynamicPermissions { get; set; }
        public List<KeyValuePair<string, string>> Claims { get; set; }
    }
}
