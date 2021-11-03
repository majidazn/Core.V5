using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class ReferenceViewModel
    {
        public long Id { get; set; }
        public long TicketId { get; set; }
        public string Title { get; set; }
        public int Code { get; set; }
        public string Comment { get; set; }
        public int ForceId { get; set; }
        public string ForceName { get; set; }
        public int KindId { get; set; }
        public string KindName { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int UnitId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool HasAttachment { get; set; }
        public string ReceiverNames { get; set; }
        public int ActivityGroup { get; set; }
        public string ActivityGroupText { get; set; }
        public byte RecieverIsRead { get; set; }
        public Boolean IsRead { get; set; }
        public int SentReferenceType { get; set; }
        public string ReceiverNote { get; set; }
        public string TicketName { get; set; }
        public string PersonFamilyName { get; set; }
        public string PersonName { get; set; }
        public long PersonId { get; set; }
        public List<ReceiverViewModel> Recievers { get; set; }
        public List<AttachmentViewModel> Attachments { get; set; }
        public List<ReminderViewModel> Reminders { get; set; }
        public List<WorkDownViewModel> WorkDown { get; set; }
    }
}
