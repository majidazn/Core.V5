using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class DashboardFilterViewModel
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string ActivityGroup { get; set; }
        public string SubActivityName { get; set; }
        public int ReportType { get; set; }
    }
}
