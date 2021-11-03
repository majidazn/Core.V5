using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SecurityHeaders
{
    public static class XRobotTag
    {
        public static void Configurations(IApplicationBuilder app)
        {
            app.UseXRobotsTag(options => options.NoIndex().NoFollow());
        }
    }
}
