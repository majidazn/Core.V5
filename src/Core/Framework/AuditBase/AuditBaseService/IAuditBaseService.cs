using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace Framework.AuditBase.AuditBaseService
{
    public interface IAuditBaseService
    {
        void SetAuditBase(EntityEntry entityEntry, int personId);
        void SetAuditId(EntityEntry entityEntry);
    }
}
