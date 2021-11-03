using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AuditBase.ViewModel
{
    public class SlogUIViewModel
    {
        public string OperatorName { get; set; }
        public string OperatorDisplayName { get; set; }
        public EntityStateType State { get; set; }
        public string IPAddress { get; set; }
        public DateTimeOffset ActDateTime { get; set; }
    }
}
