using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AuditBase.ViewModel
{
    public class SlogViewModel
    {
        public int TenantId { get; set; }
        public int? OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OperatorDisplayName { get; set; }
        public byte State { get; set; }
        public string IPAddress { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
