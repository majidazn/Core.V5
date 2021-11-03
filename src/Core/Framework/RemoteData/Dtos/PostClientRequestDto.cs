using System;
using System.Net.Http;
using System.Text;

namespace Framework.RemoteData.Dtos
{
    public class PostClientRequestDto<T>
    {
        public IHttpClientFactory ClientFactory { get; set; }
        public string ClientName { get; set; }
        public string BaseUrl { get; set; }
        public string ApiUrl { get; set; }
        public T InputModel { get; set; }
        public string Token { get; set; }
        public bool EncryptSender { get; set; }
        public bool EncryptReceiver { get; set; }
        public bool NeedStringContent { get; set; } = true;
        public int TimeOut { get; set; }
        public string[] Headers { get; set; }
    }
}
