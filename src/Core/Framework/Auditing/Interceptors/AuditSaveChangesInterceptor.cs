using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Threading;
using Core.Common.Enums;
using Framework.Auditing.Services.EntityAuditService;
using Framework.Auditing.Services.AuditSourcesService;
using Framework.Auditing.Dtos;
using Framework.AuditBase.AuditBaseService;
using Framework.Extensions;
using Framework.Auditing.Contracts;

namespace Framework.Auditing.Interceptors
{
    public class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        #region fields

        private readonly IServiceProvider _service;
        private readonly Applications _applicationId;
        private readonly IEntityAuditService _entityAuditProvider;
        private readonly IAuditSourcesService _auditSourcesProvider;
        private readonly IAuditBaseService _auditBaseService;

        #endregion

        #region ctor

        public AuditSaveChangesInterceptor(IServiceProvider service, Applications applicationId)
        {
            this._service = service;
            this._applicationId = applicationId;
            this._entityAuditProvider = service.GetRequiredService<IEntityAuditService>();
            this._auditSourcesProvider = service.GetRequiredService<IAuditSourcesService>();
            this._auditBaseService = service.GetRequiredService<IAuditBaseService>();
        }

        #endregion

        #region methods
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            ApplyAudits(eventData.Context.ChangeTracker);
            return base.SavingChanges(eventData, result);
        }


        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            ApplyAudits(eventData.Context.ChangeTracker);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void ApplyAudits(ChangeTracker changeTracker)
        {
            //var changedEntries = changeTracker.Entries().
            //  Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);

            var changedEntries = _entityAuditProvider.GetChangedEntriesWithReferences(changeTracker).ToList();

            var auditEvent = new AuditEventDto();
            foreach (var entry in changedEntries)
            {
                entry.CleanString();
                entry.SetTenantId(_auditSourcesProvider.GeTenantId());
                _auditBaseService.SetAuditBase(entry, _auditSourcesProvider.GetOperatorId());
                _auditBaseService.SetAuditId(entry);

                if (entry.Entity is IAuditable)
                    auditEvent.AuditEntries.Add(_entityAuditProvider.GetAuditEntryValues(entry, changedEntries));
            }

            if (auditEvent.AuditEntries.Count > 0)
            {
                auditEvent.AuditNetwork = _auditSourcesProvider.GetAuditNetworkValues(_applicationId);
                _service.GetService<ICapPublisher>().Publish("Audit", auditEvent);
            }
        }

        #endregion

    }
}
