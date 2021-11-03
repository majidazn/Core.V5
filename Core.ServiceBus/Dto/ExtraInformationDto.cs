using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Dto
{
    public class ExtraInformationDto
    {
        #region Constructor
        public ExtraInformationDto(long employeeId,long dataKind, string code, DateTime startDate, DateTime? endDate, byte status)
        {
            EmployeeId = employeeId;
            DataKind = dataKind;
            Code = code;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
        #endregion

        #region Properties
        public long EmployeeId { get; private set; }
        public long DataKind { get; private set; }
        public string Code { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public byte Status { get; private set; }
        public AuditBaseDto Base { get; private set; }
        #endregion
    }
}
