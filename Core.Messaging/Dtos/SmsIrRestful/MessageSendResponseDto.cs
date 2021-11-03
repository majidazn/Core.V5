namespace Core.Messaging.Dtos.SmsIrRestful
{
    public class MessageSendResponseDto : BaseResponseDto
    {
        public SentSMSResponseDto[] Ids { get; set; }
        public string BatchKey { get; set; }
    }
}
