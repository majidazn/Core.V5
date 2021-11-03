using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Dto
{
    public class EducationDto
    {
        #region Constructor
        public EducationDto(long personId ,long? medicalEducationId, long educationKind, long educationLevel, string place, string nonMedicalEduName,
            DateTime startDate, DateTime? endDate, long? duration, long? durationUnit, long? relation, byte status, AuditBaseDto auditBase)
        {
            PersonId = personId;
            NonMedicalEduName = nonMedicalEduName;
            EducationKind = educationKind;
            EducationLevel = educationLevel;
            MedicalEducationId = medicalEducationId;
            Place = place;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            DurationUnit = durationUnit;
            Relation = relation;
            Status = status;
            Base = auditBase;
        }
        #endregion

        #region Properties
        public long PersonId { get; private set; }
        public long EducationKind { get; private set; }
        public long EducationLevel { get; private set; }
        public string NonMedicalEduName { get; private set; }
        public long? MedicalEducationId { get; private set; }
        public string Place { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public long? Duration { get; private set; }
        public long? DurationUnit { get; private set; }
        public long? Relation { get; private set; }
        public byte Status { get; private set; }
        public AuditBaseDto Base { get; private set; }

        #endregion
    }
}
