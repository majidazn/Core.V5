using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Person
{
    public class EmployeeViewModel 
    {
        public EmployeeViewModel()
        {
            Positions = new List<PositionViewModel>();
        }

        public int EmployeeId { get; set; }
        public int ActivityKindGvariableId { get; set; }
        public string EmployeeNumber { get; set; }
        public int NezamKindGvariableId { get; set; }
        public int NezamNo { get; set; }
        public int? Expertise { get; set; }
        public int TitleDisplay { get; set; }
        public int PersonId { get; set; }
        public virtual PersonViewModel Person { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<PositionViewModel> Positions { get; set; }
        public int TenantId { get; set; }
        public int? TherapistServiceId { get; set; }
    }
}
