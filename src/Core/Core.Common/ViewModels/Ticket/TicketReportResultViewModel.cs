using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
 
    public class TicketReportResultViewModel
    {
        public long TicketId { get; set; }
        public long TicketNumber { get;  set; }
        public int ActivityGroup { get;  set; }
        public string ActivityGroupText { get; set; }
        public int SubActivityName { get;  set; }
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
        public string CreateDatePer { get;  set; }
        public int StatusId { get; set; }
        public int TenantId { get; set; } 
        public string CenterName { get; set; }
        public int CenterId { get; set; }
        public string Status { get; set; }
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

        public bool EPDTicket { get; set; }
        public bool AllTicket { get; set; }
        public string Name { get; set; }
        //public int Reference_Reciever_ResponsibleId { get; set; }
        //public int? ReferenceId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyDateStr { get; set; }

        public List<int> CenterList { get; set; }

        public string LastEditDateStr { get; set; }
        public int LastReferenceStatus { get; set; }
        public string LastReferenceStatusStr { get; set; }
        public DateTime? ActionFromDate { get; set; }
        public DateTime? ActionToDate { get; set; }
        public int RefForceId { get; set; }

    }
}
