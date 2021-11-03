using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Dto
{
    public class TelecomDto
    {
        #region Constructor
        public TelecomDto(long personId, byte telKind, string prefix, string tellNo, string description, byte status, AuditBaseDto auditBase)
        {
            PersonId = personId;
            TelKind = telKind;
            Prefix = prefix;
            TelNo = tellNo;
            Description = description;
            Status = status;
            Base = auditBase;
        }
        #endregion

        #region Properties
        public long PersonId { get; private set; }
        public byte TelKind { get; private set; }
        public string Prefix { get; private set; }
        public string TelNo { get; private set; }
        public string Description { get; private set; }
        public byte Status { get; private set; }
        public AuditBaseDto Base { get; private set; }

        #endregion
    }
}
