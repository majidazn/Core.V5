using Core.Common.Enums;

namespace Framework.Auditing.Dtos
{
    public class AuditNetworkDto
    {
        public Applications ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int TenantId { get; set; }
        public int OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OperatorDisplayName { get; set; }
        public string HostName { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string MachineName { get; set; }
        public string RemoteIpAddress { get; set; }
        public string LocalIpAddress { get; set; }
        public string UserAgent { get; set; }
        public string ApplicationAssemblyName { get; set; }
        public string ApplicationVersion { get; set; }
    }
}
