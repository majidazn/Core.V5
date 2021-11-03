using Core.Messaging.Contracts;

namespace Core.Messaging.Dtos.SmsIrRestful.SMSTemplates
{
    public class ResetPasswordDto /*: ISmsIrMessageData*/
    {
        public string FullName { get; set; }
        public string ApplicationName { get; set; }

    }
}
