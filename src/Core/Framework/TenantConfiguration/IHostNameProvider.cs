using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.TenantConfiguration
{
    public interface IHostNameProvider
    {
        string GetHostName();
        string GetReferer();
        bool IsSSL();
    }
}
