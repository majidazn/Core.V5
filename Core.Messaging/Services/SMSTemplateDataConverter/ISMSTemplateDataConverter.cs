using Core.Messaging.Dtos.SmsIrRestful;
using Core.Messaging.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Messaging.Services.SMSTemplateDataConverter
{
    public interface ISMSTemplateDataConverter
    {
        Dictionary<SMSTemplateType, Func<UltraFastSendRequestDto<object>, UltraFastSendRequestDto<object>>> DeserializeMessageParameters { get; }
    }
}
