using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Core.Messaging.Dtos.Email
{
    public class MailRequestDto
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
    }
}
