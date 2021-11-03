using System.Collections.Generic;

namespace Framework.Auditing.Dtos
{
    public class AuditEventDto
    {
        public AuditNetworkDto AuditNetwork { get; set; }
        public List<AuditEntryDto> AuditEntries { get; set; } = new List<AuditEntryDto>();
    }
}
