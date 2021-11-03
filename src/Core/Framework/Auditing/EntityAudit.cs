using Core.Common.Enums;
using Framework.Auditing.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Framework.Auditing
{
    public class EntityAudit
    {
        public string EntityName { get; set; }
        public EntityStateType EventType { get; set; }
        public DateTimeOffset ActDateTime { get; set; }
        public Guid AuditId { get; set; }
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();

        public AuditEntryDto ToAudit()
        {
            var audit = new AuditEntryDto();

            audit.EntityName = EntityName;
            audit.State = EventType;
            audit.ActDateTime = DateTimeOffset.Now;
            audit.AuditId = AuditId;
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            return audit;
        }

    }
}
