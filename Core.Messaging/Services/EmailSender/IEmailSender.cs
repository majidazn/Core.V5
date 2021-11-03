using Core.Messaging.Dtos.Email;
using System.Threading.Tasks;

namespace Core.Messaging.Services.EmailSender
{
    public interface IEmailSender
    {
        Task<MailResponseDto> SendEmailAsync(MailRequestDto mailRequest);
    }
}
