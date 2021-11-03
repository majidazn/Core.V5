using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SecurityHeaders
{
    public static class CspInsecureRequest
    {
        public static void Configurations(this IApplicationBuilder app)
        {
            app.UseCsp(opts => opts.UpgradeInsecureRequests());
        }
    }

}
