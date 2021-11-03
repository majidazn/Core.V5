namespace Core.ServiceBus.Events.Cap
{
    public class EditSecurityUserPersonalInformationEvent
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
