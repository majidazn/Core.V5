
using Framework.Auditing.Dtos;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace Framework.Auditing.Services.EntityAuditService
{
    public interface IEntityAuditService
    {
        AuditEntryDto GetAuditEntryValues(EntityEntry entityEntry, IEnumerable<EntityEntry> entityEntries);
        IEnumerable<EntityEntry> GetChangedEntriesWithReferences(ChangeTracker changeTracker);
    }
}
