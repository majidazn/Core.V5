using Framework.SecurityHeaders;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configurations
{
    public static class SecurityHeaders
    {
        public static void AddSecurityHeaders(this IApplicationBuilder app)
        {
            Hsts.Configurations(app);
            Csp.Configurations(app,false);
            ReferrerPolicy.Configurations(app);
            XContentType.Configurations(app);
            XDownloadOptions.Configurations(app);
            Xframe.Configurations(app);
            XRobotTag.Configurations(app);
            Xxss.Configurations(app);
        }
    }
}
