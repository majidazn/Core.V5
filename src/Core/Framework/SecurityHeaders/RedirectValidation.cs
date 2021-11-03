using Framework.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Framework.SecurityHeaders
{
    public class RedirectValidation
    {
        public static void Configurations(IApplicationBuilder app, IConfiguration Configuration)
        {
            var allowedDestinations = Configuration["AllowedDestinationsRedirection"] != null
                            ? Configuration["AllowedDestinationsRedirection"].ToString().Split(",")
                            : new string[0];
         
            var allowdHttpsPorts = Configuration["HttpsPort"].ToInteger(0);
            app.UseRedirectValidation(opts =>
            {
                opts.AllowSameHostRedirectsToHttps(allowdHttpsPorts);
                //opts.AllowSameHostRedirectsToHttps(4430); Allow redirects to custom HTTPS port
                opts.AllowedDestinations(allowedDestinations);
            });
        }
        public static void Configurations(IApplicationBuilder app)
        {
            app.UseRedirectValidation();
        }
    }
}
