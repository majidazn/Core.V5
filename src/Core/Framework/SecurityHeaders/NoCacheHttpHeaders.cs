using Microsoft.AspNetCore.Builder;

namespace Framework.SecurityHeaders
{
    public class NoCacheHttpHeaders
    {
        public static void Configurations(IApplicationBuilder app)
        {
            app.UseNoCacheHttpHeaders();
        }
    }
}
