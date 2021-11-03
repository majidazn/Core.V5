using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class AttachmentViewModel
    {
        public Guid AttachmentId { get;  set; }
        public long? TicketId { get;  set; }
        public long? ReferenceId { get;  set; }
        public long UserId { get;  set; }
        public byte[] AttachmentFile { get;  set; }
        public string FileType { get;  set; }
        public string FileName { get;  set; }

        public string Note { get;  set; }
    }
}
