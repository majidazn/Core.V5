using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
 
    public class TicketViewModel
    {
        public long TicketId { get; set; }
        public long TicketNumber { get;  set; }
        public int ActivityGroup { get;  set; }
        public string ActivityGroupText { get; set; }
        public int SubActivityName { get;  set; }
        public List<int> SubActivityNameList { get; set; }
        public string SubActivityText { get; set; }
        public List<int> ProblemGroupList { get;  set; }
        public int ProblemGroup { get;  set; }
        public string ProblemGroupText { get; set; }
        public  int ForceId { get;  set; }
        public  string ForceText { get; set; }
        public  int KindId { get;  set; }
        public  string KindText{ get; set; }
        public int? ReferenceTicketNumber { get;  set; }
        public string Subject { get;  set; }
        public string Description { get;  set; }
        public long UserId { get;  set; }
        public long PersonId { get; set; }
        public string PersonFullName { get; set; }
        public DateTime? CreateDate { get;  set; }
        public int TicketStatusId { get; set; }
        public int ReferenceStatusId { get; set; }
        public int TenantId { get; set; }
        public string CenterName { get; set; }
        public string TicketStatusName { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string ReferenceStatusName { get; set; }
        public bool HasAttachment { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PositionId { get; set; }
        public DateTime LatestModifyAt { get; set; }
        public bool IsRead { get; set; }
        /// <summary>
        /// noe karbare jarie login shode
        /// </summary>
        public int? CurrentUserType { get; set; }

        //public bool EPDTicket { get; set; }
        //public bool AllTicket { get; set; }
        //public bool OnlyNewTickets { get; set; }
        //public bool AllGroupTicket { get; set; }
        public string Name { get; set; }
        public bool IsOwnTicket { get; set; }
        //public int Reference_Reciever_ResponsibleId { get; set; }
        //public int? ReferenceId { get; set; }


        public List<int> CenterList { get; set; }
        public byte RecieverIsRead { get; set; }



        public bool TicketReceiverIsRead { get; set; }
        public bool ReferenceReceiverIsRead { get; set; }
        public string ReceiverNote { get; set; }
        public bool HasRelativeTicket { get; set; }
        public int CenterId { get; set; }

        public int SentTicketType { get; set; }
        public bool CanDeleteOrUpdate { get; set; }

        public int TopGridButtonsTicketType { get; set; }
        public int EpdSectionId { get; set; }
        public string EpdSectionName { get; set; }

        public int StateId { get; set; }
        public int CenterGroup { get; set; }
        public int TicketStatus { get; set; }
        public DateTime? ModifyDate { get;  set; }
        public string PersonName { get; set; }
        public string PersonFamilyName { get; set; }
        public List<ReferenceViewModel>  References { get; set; }
        public List<ReceiverViewModel> Recievers { get; set; }
        public List<AttachmentViewModel> Attachments { get; set; }
        public List<ReminderViewModel> Reminders { get; set; }

    }
}
