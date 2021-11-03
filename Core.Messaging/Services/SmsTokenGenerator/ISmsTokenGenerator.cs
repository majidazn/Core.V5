using Core.Messaging.Dtos.SmsIrRestful;
using System.Threading.Tasks;

namespace Core.Messaging.Services.SmsTokenGenerator
{
    public interface ISmsTokenGenerator
    {
        Task<TokenResultDto> GetToken();
    }
}
