namespace Core.Messaging.Dtos.SmsIrRestful
{
    public class TokenResultDto
    {
        public string TokenKey { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
