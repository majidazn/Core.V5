using Core.Common.Enums;
using System;

namespace Framework.Auditing.Contracts
{
    public interface IAuditable
    {
        Guid AuditId { get; }
        EntityStateType Status { get; }
    }
}
