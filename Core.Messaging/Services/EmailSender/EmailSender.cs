using Core.Messaging.Dtos.Email;
using Core.Messaging.Infrastructures;
using MimeKit;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messaging.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public async Task<MailResponseDto> SendEmailAsync(MailRequestDto mailRequest)
        {
            MailResponseDto result;
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(mailRequest.ToEmail));  // replace with valid value 
            mail.From = new MailAddress(MailSettingProvider.Mail);  // replace with valid value
            mail.Subject = mailRequest.Subject;
            mail.Body = mailRequest.Body;
            mail.IsBodyHtml = mailRequest.IsBodyHtml;
            mail.BodyEncoding = Encoding.UTF8;
            //mail.Headers.Add("List-Unsubscribe", "<mailto:unsub@epd-co.com?subject=unsubscribe>");

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = MailSettingProvider.Mail,  // replace with valid value
                    Password = MailSettingProvider.Password  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = MailSettingProvider.Host;
                smtp.Port = MailSettingProvider.Port;
                smtp.EnableSsl = false;
                smtp.Timeout = 5000;

                try
                {
                    await smtp.SendMailAsync(mail);
                    result = new MailResponseDto
                    {
                        IsSuccessful = true,
                        Message = "ارسال ایمیل با موفقیت انجام شد"
                    };
                }
                catch (System.Exception)
                {
                    result = new MailResponseDto
                    {
                        IsSuccessful = false,
                        Message = "ارسال ایمیل با موفقیت انجام نشد"
                    };
                }
            }
            return result;
        }

        private BodyBuilder AddAttachments(MailRequestDto mailRequest)
        {
            var builder = new BodyBuilder();
            if (mailRequest.Attachments.Count > 0)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            return builder;
        }
    }
}
