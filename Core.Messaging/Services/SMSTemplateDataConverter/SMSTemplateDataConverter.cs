using Core.Messaging.Dtos.SmsIrRestful;
using Core.Messaging.Dtos.SmsIrRestful.SMSTemplates;
using Core.Messaging.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core.Messaging.Services.SMSTemplateDataConverter
{
    public class SMSTemplateDataConverter : ISMSTemplateDataConverter
    {
        public Dictionary<SMSTemplateType, Func<UltraFastSendRequestDto<object>, UltraFastSendRequestDto<object>>> DeserializeMessageParameters
        {
            get => new Dictionary<SMSTemplateType, Func<UltraFastSendRequestDto<object>, UltraFastSendRequestDto<object>>>
            {
                { SMSTemplateType.ResetPassword, DeserializeResetPassword },
                { SMSTemplateType.RegisterUserDto, DeserializeRegisterUser },
                { SMSTemplateType.LoginSecurityCode, DeserializeLoginSecurityCode },
                { SMSTemplateType.ExperimentsResult ,  DeserializeExperimentsResultDto },
            };
        }

        private UltraFastSendRequestDto<object> DeserializeResetPassword(UltraFastSendRequestDto<object> parameters)
        {
            parameters.MessageParameters = JsonConvert.DeserializeObject<ResetPasswordDto>(parameters.MessageParameters.ToString());
            return parameters;
        }

        private UltraFastSendRequestDto<object> DeserializeRegisterUser(UltraFastSendRequestDto<object> parameters)
        {
            parameters.MessageParameters = JsonConvert.DeserializeObject<RegisterUserDto>(parameters.MessageParameters.ToString());
            return parameters;
        }

        private UltraFastSendRequestDto<object> DeserializeLoginSecurityCode(UltraFastSendRequestDto<object> parameters)
        {
            parameters.MessageParameters = JsonConvert.DeserializeObject<LoginSecurityCodeDto>(parameters.MessageParameters.ToString());
            return parameters;
        }

        private UltraFastSendRequestDto<object> DeserializeExperimentsResultDto(UltraFastSendRequestDto<object> parameters)
        {
            parameters.MessageParameters = JsonConvert.DeserializeObject<ExperimentsResultDto>(parameters.MessageParameters.ToString());
            return parameters;
        }
    }
}
