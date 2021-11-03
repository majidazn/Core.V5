using System.Collections.Generic;

namespace Core.Common.ViewModels.Account
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public int TenantId { get; set; }
        public string Hostname { get; set; }
        public int PersonId { get; set; }
        public List<int> AccessTenantIds { get; set; }
    }
}
