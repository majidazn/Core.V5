using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Framework.SecurityHeaders
{
    public static class Xxss
    {
        public static void Configurations(IApplicationBuilder app)
        {
            app.UseXXssProtection(opts => opts.EnabledWithBlockMode());
        }
    }
}
