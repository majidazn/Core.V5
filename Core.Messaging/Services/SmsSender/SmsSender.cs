using Core.Messaging.Dtos.SmsIrRestful;
using Core.Messaging.Services.SMSTemplateDataConverter;
using Core.Messaging.Services.SmsTokenGenerator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messaging.Services.SmsSender
{
    public class SmsSender : ISmsSender
    {
        private readonly ISmsTokenGenerator _smsTokenGenerator;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISMSTemplateDataConverter _sMSTemplateDataConverter;

        public SmsSender(
            ISmsTokenGenerator smsTokenGenerator,
            IHttpClientFactory httpClientFactory,
            ISMSTemplateDataConverter sMSTemplateDataConverter)
        {
            _smsTokenGenerator = smsTokenGenerator;
            _httpClientFactory = httpClientFactory;
            this._sMSTemplateDataConverter = sMSTemplateDataConverter;
        }

        public async Task<MessageSendResponseDto> SendAsync(MessageSendRequestDto messageDto)
        {
            MessageSendResponseDto result = new MessageSendResponseDto();
            try
            {
                var token = await _smsTokenGenerator.GetToken();
                if (!token.IsSuccessful)
                    return result;

                result = await SendAsync(token.TokenKey, messageDto);
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        public async Task<UltraFastSendResponeDto> UltraFastSendAsync<MessageDto>(UltraFastSendRequestDto<MessageDto> parameters)/* where MessageDto : ISmsIrMessageData*/
        {
            UltraFastSendResponeDto result = new UltraFastSendResponeDto();
            try
            {
                var token = await _smsTokenGenerator.GetToken();
                if (!token.IsSuccessful)
                    return result;

                result = await SendAsync(token.TokenKey, parameters);
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        public async Task<UltraFastSendResponeDto> UltraFastSendAsync(UltraFastSendRequestDto<object> parameters)
        {
            UltraFastSendResponeDto result = new UltraFastSendResponeDto();
            try
            {
                var token = await _smsTokenGenerator.GetToken();
                if (!token.IsSuccessful)
                    return result;

                _sMSTemplateDataConverter.DeserializeMessageParameters[parameters.SmsTemplateType](parameters);

                result = await SendAsync(token.TokenKey, parameters);
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        private async Task<MessageSendResponseDto> SendAsync(string token, MessageSendRequestDto messageDto)
        {
            MessageSendResponseDto result = new MessageSendResponseDto();
            HttpClient httpClient = _httpClientFactory.CreateClient("SmsIr");
            httpClient.DefaultRequestHeaders.Add("x-sms-ir-secure-token", token);

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(messageDto), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await httpClient.PostAsync("/api/MessageSend", httpContent);
            if (httpResponse.IsSuccessStatusCode)
                result = JsonConvert.DeserializeObject<MessageSendResponseDto>(await httpResponse.Content.ReadAsStringAsync());

            return result;
        }

        private async Task<UltraFastSendResponeDto> SendAsync<MessageDto>(string token, UltraFastSendRequestDto<MessageDto> ultraFastSendRequestDto)/* where MessageDto : ISmsIrMessageData*/
        {
            UltraFastSendResponeDto result = new UltraFastSendResponeDto();
            HttpClient httpClient = _httpClientFactory.CreateClient("SmsIr");
            httpClient.DefaultRequestHeaders.Add("x-sms-ir-secure-token", token);

            List<UltraFastParametersDto> ultraFastParameters = new List<UltraFastParametersDto>();
            foreach (var propertyInfo in ultraFastSendRequestDto.MessageParameters.GetType().GetProperties())
            {
                ultraFastParameters.Add(new UltraFastParametersDto()
                {
                    Parameter = propertyInfo.Name,
                    ParameterValue = (string)propertyInfo.GetValue(ultraFastSendRequestDto.MessageParameters)
                });
            }

            UltraFastSendDto content = new UltraFastSendDto
            {
                ParameterArray = ultraFastParameters,
                Mobile = ultraFastSendRequestDto.PhoneNumber,
                TemplateId = (int)ultraFastSendRequestDto.SmsTemplateType
            };

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await httpClient.PostAsync("/api/UltraFastSend", httpContent);
            if (httpResponse.IsSuccessStatusCode)
                result = JsonConvert.DeserializeObject<UltraFastSendResponeDto>(await httpResponse.Content.ReadAsStringAsync());

            return result;
        }

        private async Task<UltraFastSendResponeDto> SendAsync(string token, UltraFastSendRequestDto<object> ultraFastSendRequestDto)
        {
            UltraFastSendResponeDto result = new UltraFastSendResponeDto();
            HttpClient httpClient = _httpClientFactory.CreateClient("SmsIr");
            httpClient.DefaultRequestHeaders.Add("x-sms-ir-secure-token", token);

            List<UltraFastParametersDto> ultraFastParameters = new List<UltraFastParametersDto>();
            foreach (var propertyInfo in ultraFastSendRequestDto.MessageParameters.GetType().GetProperties())
            {
                ultraFastParameters.Add(new UltraFastParametersDto()
                {
                    Parameter = propertyInfo.Name,
                    ParameterValue = (string)propertyInfo.GetValue(ultraFastSendRequestDto.MessageParameters)
                });
            }

            UltraFastSendDto content = new UltraFastSendDto
            {
                ParameterArray = ultraFastParameters,
                Mobile = ultraFastSendRequestDto.PhoneNumber,
                TemplateId = (int)ultraFastSendRequestDto.SmsTemplateType
            };

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await httpClient.PostAsync("/api/UltraFastSend", httpContent);
            if (httpResponse.IsSuccessStatusCode)
                result = JsonConvert.DeserializeObject<UltraFastSendResponeDto>(await httpResponse.Content.ReadAsStringAsync());

            return result;
        }
    }
}
