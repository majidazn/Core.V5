using Core.Messaging.Dtos.SmsIrRestful;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messaging.Services.SmsTokenGenerator
{
    public class SmsTokenGenerator : ISmsTokenGenerator
    {
        private const string userApiKey = "2b28e0c8da97b04112ee0c8";
        private const string secretKey = "ep@d^e*c()85P@s%";
        private const string cacheKey = "SmsIrToken";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;

        public SmsTokenGenerator(
            IHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache)
        {
            this._httpClientFactory = httpClientFactory;
            this._memoryCache = memoryCache;
        }

        public async Task<TokenResultDto> GetToken()
        {
            TokenResultDto data;
            if (_memoryCache.TryGetValue(cacheKey, out data))
                return data;           
            else
            {
                data = await GenerateToken();
                if (data.IsSuccessful)
                {
                    _memoryCache.Set(cacheKey, data, TimeSpan.FromMinutes(28));
                    return data;
                }
                return data;
            }
        }

        private async Task<TokenResultDto> GenerateToken()
        {
            TokenResultDto result = new TokenResultDto();
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("SmsIr");

                TokenRequestDto content = new TokenRequestDto
                {
                    UserApiKey = userApiKey,
                    SecretKey = secretKey
                };

                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await httpClient.PostAsync("/api/Token", httpContent);
                if (httpResponse.IsSuccessStatusCode)
                    result = JsonConvert.DeserializeObject<TokenResultDto>(await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
    }
}
