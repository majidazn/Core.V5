using Core.Common.Enums;
using System;

namespace Framework.AuditBase.Dtos
{
    public class EntityRawSlogsDto
    {
        public Applications ApplicationId { get; set; }
        public string EntityName { get; set; }
        public Guid AuditId { get; set; }
    }
}
