using Core.Messaging.Contracts;
using Core.Messaging.Enums;

namespace Core.Messaging.Dtos.SmsIrRestful
{
    public class UltraFastSendRequestDto<MessageDto> /*where MessageDto : ISmsIrMessageData*/
    {
        public SMSTemplateType SmsTemplateType { get; set; }
        public long PhoneNumber { get; set; }
        public MessageDto MessageParameters { get; set; }
    }
}
