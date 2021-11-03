using Newtonsoft.Json.Linq;

namespace Core.Common.ViewModels.Account
{
    public class RefreshTokenModel
    {
        public LoginModel LoginModel { get; set; }
        public JToken JToken { get; set; }
    }
}
