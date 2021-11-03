using Core.Common.Enums;
using System;

namespace Framework.Auditing.Dtos
{
    public class AuditEntryDto
    {
        public string EntityName { get; set; }
        public EntityStateType State { get; set; }
        public DateTimeOffset ActDateTime { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public Guid AuditId { get; set; }
    }
}
