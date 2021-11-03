using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class AssessmentViewModel
    {
        public long Id { get; set; }
        public long TicketId { get; set; }
        public int ProblemGroup { get; set; }
        public int KnownErrorId { get; set; }
        public int HardnessId { get; set; }
        public int HumanErrorId { get; set; }
        public int TicketAffectId { get; set; }
        public int WorkQualityId { get; set; }
        public string Comment { get; set; }
    }
}
