using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Framework.RemoteData.Dtos
{
    public class GetClientRequestDto
    {
        public IHttpClientFactory ClientFactory { get; set; }
        public string BaseUrl { get; set; }
        public string ClientName { get; set; }
        public string ApiUrl { get; set; }
        public List<KeyValuePair<string, string>> InputModel { get; set; }
        public string Token { get; set; }
        public bool EncryptReceiver { get; set; }
        public bool EncryptSender { get; set; }
        public bool NeedStringContent { get; set; }
        public int Timeout { get; set; }
        public string[] Headers { get; set; }
    }
}
