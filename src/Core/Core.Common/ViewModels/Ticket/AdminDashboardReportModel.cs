using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class AdminDashboardReportModel
    {
        public int Diff { get; set; }
        public DateTime MaxDate { get; set; }
        public long Id { get; set; }
        public string CenterName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string tForce { get; set; }
        public int ForceId { get; set; }
        public int StatusId { get; set; }
        public string tStatus { get; set; }
        public string tProblem { get; set; }
        public int ProblemGroup { get; set; }
        public DateTime BaseCreateDate { get; set; }
        public DateTime LatestModifyAt { get; set; }
        public string rForce { get; set; }
        public string rStatus { get; set; }
        public int rStatusId { get; set; }
        public bool rRead { get; set; }
        public bool IsRead { get; set; }
        public int ActivityGroup { get; set; }
        public int SubActivityName { get; set; }
        public long UserId { set; get; }
        public string gName { get; set; }
        public string sgName { get; set; }

    }
}
