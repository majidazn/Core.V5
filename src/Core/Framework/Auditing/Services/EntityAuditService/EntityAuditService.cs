using Core.Common.Enums;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using Framework.Auditing.Attributes;
using Framework.Auditing.Contracts;
using Framework.Auditing.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Auditing.Services.EntityAuditService
{
    public class EntityAuditService : IEntityAuditService
    {
        #region fields

        #endregion

        #region ctor

        #endregion

        #region Methods

        public AuditEntryDto GetAuditEntryValues(EntityEntry entityEntry, IEnumerable<EntityEntry> entityEntries)
        {
            var auditEntry = new EntityAudit();

            auditEntry.EntityName = entityEntry.Entity.GetType().Name;
            auditEntry.EventType = SetEventType(entityEntry);

            SetEntityEntryAuditValuesInModifiedState(entityEntry, auditEntry);
            SetValueObjectsAuditValuesInModifiedState(entityEntry, entityEntries, auditEntry);
            SetValueObjectsAuditValuesInAddedState(entityEntry, auditEntry);

            return auditEntry.ToAudit();
        }

        private void SetEntityEntryAuditValuesInModifiedState(EntityEntry entityEntry, EntityAudit auditEntry)
        {
            foreach (var propertyEntry in entityEntry.Properties)
            {
                var propertyType = propertyEntry.Metadata.PropertyInfo;
                if (Attribute.IsDefined(propertyType, typeof(DisableAuditingAttribute)))
                    continue;

                string propertyName = propertyEntry.Metadata.Name;

                if (propertyName == nameof(IAuditable.AuditId))
                {
                    auditEntry.AuditId = (Guid)propertyEntry.CurrentValue;
                    continue;
                }

                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        SetAddedValues(auditEntry, propertyEntry, propertyName);
                        break;
                    case EntityState.Deleted:
                        SetDeletedValues(auditEntry, propertyEntry, propertyName);
                        break;
                    case EntityState.Modified:
                        SetModifiedValues(auditEntry, propertyEntry, propertyName);
                        break;
                }
            }
        }

        private void SetValueObjectsAuditValuesInModifiedState(EntityEntry entityEntry, IEnumerable<EntityEntry> entityEntries, EntityAudit auditEntry)
        {
            if (entityEntry.State == EntityState.Unchanged || entityEntry.State == EntityState.Modified)
            {
                foreach (var referenceEntry in entityEntry.References)
                {
                    //if (referenceEntry?.TargetEntry?.Entity is ValueObject)
                    if (referenceEntry?.TargetEntry?.Entity is IAuditableReference)
                        foreach (var propertyEntry in referenceEntry.TargetEntry.Properties)
                        {
                            if (referenceEntry.TargetEntry.Properties.FirstOrDefault().Metadata.Name == propertyEntry.Metadata.Name)
                                continue;

                            var propertyType = propertyEntry.Metadata.PropertyInfo;
                            if (Attribute.IsDefined(propertyType, typeof(DisableAuditingAttribute)))
                                continue;

                            var finfOldReferenceProperty = entityEntries.FirstOrDefault(i => i.Metadata.ClrType.FullName == referenceEntry.Metadata.ClrType.FullName &&
                            i.Properties.FirstOrDefault().CurrentValue.Equals(referenceEntry.TargetEntry.Properties.FirstOrDefault().CurrentValue));

                            string propertyName = propertyEntry.Metadata.Name;
                            var oldProperty = finfOldReferenceProperty?.Property(propertyName);

                            if (oldProperty == null)
                                continue;

                            SetValueObjectsPropertyValues(auditEntry, propertyEntry, oldProperty, $"{referenceEntry.Metadata.Name}_{propertyName}");
                        }
                }
            }
        }

        private void SetValueObjectsAuditValuesInAddedState(EntityEntry entityEntry, EntityAudit auditEntry)
        {
            if (entityEntry.State == EntityState.Added)
            {
                foreach (var referenceEntry in entityEntry.References)
                {
                    if (referenceEntry?.TargetEntry?.Entity is IAuditableReference)
                        foreach (var propertyEntry in referenceEntry.TargetEntry.Properties)
                        {
                            if (referenceEntry.TargetEntry.Properties.First().Metadata.Name == propertyEntry.Metadata.Name)
                                continue;

                            var entityType = propertyEntry.Metadata.PropertyInfo;
                            if (Attribute.IsDefined(entityType, typeof(DisableAuditingAttribute)))
                                continue;

                            SetAddedValues(auditEntry, propertyEntry, $"{referenceEntry.Metadata.Name}_{propertyEntry.Metadata.Name}");
                        }

                }
            }
        }

        private EntityStateType SetEventType(EntityEntry entityEntry)
        {
            EntityStateType eventType = EntityStateType.Modified;
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    eventType = EntityStateType.Default;
                    break;
                case EntityState.Deleted:
                    eventType = EntityStateType.PhysicalDelete;
                    break;
                case EntityState.Modified:
                    if ((EntityStateType)entityEntry.Property("Status").CurrentValue == EntityStateType.Deleted)
                        eventType = EntityStateType.Deleted;
                    else
                        eventType = EntityStateType.Modified;
                    break;
            }

            return eventType;
        }

        private void SetValueObjectsPropertyValues(EntityAudit auditEntry, PropertyEntry property, PropertyEntry oldProperty, string propertyName)
        {
            if (oldProperty.CurrentValue != property.CurrentValue)
            {
                auditEntry.OldValues[propertyName] = oldProperty.CurrentValue;
                auditEntry.NewValues[propertyName] = property.CurrentValue;
            }
        }

        private void SetModifiedValues(EntityAudit auditEntry, PropertyEntry property, string propertyName)
        {
            if (property.IsModified)
            {
                auditEntry.OldValues[propertyName] = property.OriginalValue;
                auditEntry.NewValues[propertyName] = property.CurrentValue;
            }
        }

        private void SetDeletedValues(EntityAudit auditEntry, PropertyEntry property, string propertyName)
            => auditEntry.OldValues[propertyName] = property.OriginalValue;

        private void SetAddedValues(EntityAudit auditEntry, PropertyEntry property, string propertyName)
            => auditEntry.NewValues[propertyName] = property.CurrentValue;

        public IEnumerable<EntityEntry> GetChangedEntriesWithReferences(ChangeTracker changeTracker)
        {
            var changedEntries = changeTracker.Entries().
                Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted ||
                x.References.Any(r => r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned() && Extensions.IsModified(r.TargetEntry)));

            return changedEntries;
        }

        private IEnumerable<EntityEntry> GetChangedEntriesAfterSetAuditBase(IEnumerable<EntityEntry> entityEntries)
        {
            var changedEntriesAfterSetAuditBase = entityEntries.Where(i => i.State == EntityState.Deleted &&
                 i.Metadata.ClrType.GetInterfaces().Contains(typeof(Contracts.IAuditableReference)));

            return changedEntriesAfterSetAuditBase;
        }

        #endregion

    }

    public static class Extensions
    {
        public static bool IsModified(this EntityEntry entry) =>
            entry.State == EntityState.Added || entry.State == EntityState.Modified ||
            entry.References.Any(r => r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned() && IsModified(r.TargetEntry));
    }
}