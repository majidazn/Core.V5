using Core.Common.Enums;
using Framework.Auditing.Dtos;

namespace Framework.Auditing.Services.AuditSourcesService
{
    public interface IAuditSourcesService
    {
        AuditNetworkDto GetAuditNetworkValues(Applications applicationId);
        int GeTenantId();
        int GetOperatorId();
    }
}
