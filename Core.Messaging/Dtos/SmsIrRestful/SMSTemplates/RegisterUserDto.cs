using Core.Messaging.Contracts;

namespace Core.Messaging.Dtos.SmsIrRestful.SMSTemplates
{
    public class RegisterUserDto/* : ISmsIrMessageData*/
    {
        public string FullName { get; set; }
        public string ApplicationName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
