using Framework.TenantConfiguration;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SecurityHeaders
{
    public static class Csp
    {
        public static void Configurations(this IApplicationBuilder app, bool isSSL)
        {
            app.UseCsp(opts =>
            {
                opts.BlockAllMixedContent()
                   .DefaultSources(s => s.Self())
                   .StyleSources(x => x.Self())
                   .StyleSources(x => x.UnsafeInline())
                   .FontSources(x => x.Self())
                   .FormActions(x => x.Self())
                   .FrameAncestors(x => x.Self())
                   .ImageSources(x => x.Self())
                   .ScriptSources(x => x.Self());

                if (isSSL)
                    opts.UpgradeInsecureRequests();

                opts.ReportUris(x => x.Uris("/report"));
            });

            app.UseCspReportOnly(options => options
             .DefaultSources(s => s.Self())
             .ImageSources(s => s.Self()));
        }
    }
}
