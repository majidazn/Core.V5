using Core.Messaging.Enums;
using System;
using System.Collections.Generic;

namespace Core.Messaging.Dtos.SmsIrRestful
{
    public class MessageSendRequestDto
    {
        public List<string> Messages { get; set; }
        public List<string> MobileNumbers { get; set; }
        public string LineNumber { get; private set; } = LineNumberType.SmsIr.ToString("d");
        public DateTime? SendDateTime { get; set; }
        public bool? CanContinueInCaseOfError { get; set; }
    }
}
