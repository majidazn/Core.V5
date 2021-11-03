using Core.Messaging.Services.EmailSender;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Messaging.Infrastructures
{
    public static class EmailConfigureServices
    {
        public static void AddMailMessaging(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
