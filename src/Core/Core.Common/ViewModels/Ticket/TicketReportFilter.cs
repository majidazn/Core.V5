using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class TicketReportFilter
    {
        public string StateId { get; set; }
        public string CenterGroupId { get; set; }
        public string CenterId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string StatusId { get; set; }
        public string ForceId { get; set; }
        public string Subject { get; set; }
        public string ProblemGroup { get; set; }
        public string ActivityGroup { get; set; }
        public string SubActivityName { get; set; }
        public DateTime? ActionFromDate { get; set; }
        public DateTime? ActionToDate { get; set; }
        public string RefStatusId { get; set; }
        public int TicketStatus { get; set; }
        public string RefForceId { get; set; }
        public string LastReferenceName { get; set; }
    }
}
