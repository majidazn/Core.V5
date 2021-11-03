using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Dto
{
    public class EmployeeDto
    {
        #region Constructor
        public EmployeeDto(long personId, long mainActivityKind, string employeeNumber, long? nezamKind, string nezamNumber, long titleDisplay, DateTime startDate, DateTime? endDate,
            long? therapistServiceId, long expertise, bool isDisabled, byte status, AuditBaseDto auditBas, List<ActivityKindDto> activityKinds, List<ExtraInformationDto> extraInformationDto)
        {
            PersonId = personId;
            MainActivityKind = mainActivityKind;
            EmployeeNumber = employeeNumber;
            NezamKind = nezamKind;
            NezamNumber = nezamNumber;
            TitleDisplay = titleDisplay;
            StartDate = startDate;
            EndDate = endDate;
            TherapistServiceId = therapistServiceId;
            IsDisable = isDisabled;
            Status = status;
            Expertise = expertise;
            Base = auditBas;
            ActivityKinds = activityKinds;
            ExtraInformation = extraInformationDto;
        }
        #endregion

        #region Properties
        public long PersonId { get; private set; }

        public long MainActivityKind { get; private set; }

        public string EmployeeNumber { get; private set; }

        public long? NezamKind { get; private set; }

        public string NezamNumber { get; private set; }

        public long TitleDisplay { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public long? TherapistServiceId { get; private set; }

        public long Expertise { get; private set; }

        public bool IsDisable { get; private set; }

        public byte Status { get; private set; }

        public AuditBaseDto Base { get; private set; }
        public List<ActivityKindDto> ActivityKinds { get; private set; }
        public List<ExtraInformationDto> ExtraInformation { get; private set; }

        #endregion
    }
}
