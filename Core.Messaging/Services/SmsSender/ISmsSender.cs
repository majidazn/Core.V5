using Core.Messaging.Contracts;
using Core.Messaging.Dtos.SmsIrRestful;
using Core.Messaging.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Messaging.Services.SmsSender
{
    public interface ISmsSender
    {
        Task<MessageSendResponseDto> SendAsync(MessageSendRequestDto messageDto);
        Task<UltraFastSendResponeDto> UltraFastSendAsync<MessageDto>(UltraFastSendRequestDto<MessageDto> ultraFastSendRequestDto); /*where MessageDto : ISmsIrMessageData;*/
        Task<UltraFastSendResponeDto> UltraFastSendAsync(UltraFastSendRequestDto<object> parameters);
    }
}
