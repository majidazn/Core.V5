using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Dto
{
    public class ActivityKindDto
    {
        #region Constructor
        public ActivityKindDto(long employeeId, long activity, long place, DateTime startDate, DateTime? endDate, byte status)
        {
            EmployeeId = employeeId;
            Activity = activity;
            Place = place;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
        #endregion

        #region Properties
        public long EmployeeId { get; private set; }
        public long Activity { get; private set; }
        public long Place { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public byte Status { get; private set; }
        public AuditBaseDto Base { get; private set; }
        #endregion
    }
}
