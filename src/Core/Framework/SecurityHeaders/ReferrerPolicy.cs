using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SecurityHeaders
{
    public static class ReferrerPolicy
    {
        public static void Configurations(IApplicationBuilder app)
        {
            app.UseReferrerPolicy(opts => opts.NoReferrer());
        }
    }
}
