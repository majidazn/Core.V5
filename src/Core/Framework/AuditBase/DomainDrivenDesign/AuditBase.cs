using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Framework.AuditBase.DomainDrivenDesign
{
    public class AuditBase : ValueObject
    {

        #region Constructors

        protected AuditBase() { }

        public AuditBase(DateTimeOffset createdDate, DateTimeOffset lastModifiedDate, int? operatorId)
        {
            this.CreatedDate = createdDate;
            this.LastModifiedDate = lastModifiedDate;
            this.OperatorId = operatorId;
        }

        #endregion

        #region Properties

        public int? OperatorId { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public DateTimeOffset LastModifiedDate { get; private set; }

        #endregion

        #region Behaviors

        public AuditBase SetModifiedDateAndSLog(DateTimeOffset lastModifiedDate)
        {
            return new AuditBase(this.CreatedDate, lastModifiedDate, this.OperatorId);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CreatedDate;
            yield return LastModifiedDate;
            yield return OperatorId;
        }

        #endregion
    }
}
