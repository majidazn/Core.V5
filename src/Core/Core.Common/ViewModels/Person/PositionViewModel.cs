using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Person
{
    public class PositionViewModel
    {
        public int PositionId { get; set; }
        public int EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }
        public int PositionTitle { get; set; }
        public int JobGvariableId { get; set; }
        public byte OnCall { get; set; }
        public int ServiceKindGvariableId { get; set; }
        public byte Main { get; set; }
        public int Level { get; set; }
        public byte Academic { get; set; }
        public int Shift { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}