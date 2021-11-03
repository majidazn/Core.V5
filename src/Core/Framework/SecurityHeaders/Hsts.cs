using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SecurityHeaders
{
    public static class Hsts
    {
        public static void Configurations(IApplicationBuilder app)
        {
                app.UseHsts(opts => opts.MaxAge(365)
                .IncludeSubdomains()
                .Preload());
        }
    }
}
