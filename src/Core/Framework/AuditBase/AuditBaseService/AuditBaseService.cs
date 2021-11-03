using Framework.Auditing.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace Framework.AuditBase.AuditBaseService
{
    public class AuditBaseService : IAuditBaseService
    {
        #region Fields


        #endregion

        #region Ctor


        #endregion

        #region Methods

        public void SetAuditBase(EntityEntry entityEntry, int personId)
        {
            var referenceProperty = entityEntry.References.FirstOrDefault(i => i.Metadata.ClrType == typeof(DomainDrivenDesign.AuditBase));
            if (referenceProperty != null)
            {
                var dateTimeOffsetNow = DateTimeOffset.Now;
                if (entityEntry.State == EntityState.Added)
                {
                    DomainDrivenDesign.AuditBase auditBase = new DomainDrivenDesign.AuditBase(dateTimeOffsetNow, dateTimeOffsetNow, personId);
                    entityEntry.Reference(nameof(AuditBase)).CurrentValue = auditBase;
                }
                else if (entityEntry.State == EntityState.Modified || entityEntry.State == EntityState.Deleted || entityEntry.State == EntityState.Unchanged)
                {
                    if (entityEntry.Reference(nameof(AuditBase)).CurrentValue == null)
                    {
                        DomainDrivenDesign.AuditBase auditBase = new DomainDrivenDesign.AuditBase(dateTimeOffsetNow, dateTimeOffsetNow, personId);
                        entityEntry.Reference(nameof(AuditBase)).CurrentValue = auditBase;
                    }
                    else
                        entityEntry.Reference(nameof(AuditBase)).CurrentValue =
                            ((DomainDrivenDesign.AuditBase)referenceProperty.CurrentValue).SetModifiedDateAndSLog(dateTimeOffsetNow);
                }
            }
        }

        public void SetAuditId(EntityEntry entityEntry)
        {
            if (entityEntry.Metadata.FindProperty(nameof(IAuditable.AuditId)) != null)
                entityEntry.Property(nameof(IAuditable.AuditId)).CurrentValue = Guid.NewGuid();
        }

        #endregion
    }
}
