using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Dto
{
    public class AuditBaseDto
    {
        #region Constructor
        public AuditBaseDto(string operatorName, int? operatorId, DateTime createdDate, DateTime lastModifiedDate)
        {
            OperatorName = operatorName;
            OperatorId = operatorId;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
        }
        #endregion

        #region Prperties
        public string OperatorName { get; private set; }
        public int? OperatorId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        #endregion
    }
}
