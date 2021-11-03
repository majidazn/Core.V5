using Core.Messaging.Enums;
using System.Collections.Generic;

namespace Core.Messaging.Dtos.SmsIrRestful
{
    public class UltraFastSendDto
    {
        public List<UltraFastParametersDto> ParameterArray { get; set; }
        public long Mobile { get; set; }
        public int TemplateId { get; set; }
    }

    public class UltraFastParametersDto
    {
        public string Parameter { get; set; }
        public string ParameterValue { get; set; }
    }
}
