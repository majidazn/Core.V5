using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SecurityHeaders
{
    public static class Xframe
    {
        public static void Configurations(IApplicationBuilder app)
        {
            app.UseXfo(opts => opts.SameOrigin());
        }
    }
}
