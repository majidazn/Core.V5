using Core.Messaging.Services.SmsSender;
using Core.Messaging.Services.SMSTemplateDataConverter;
using Core.Messaging.Services.SmsTokenGenerator;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Messaging.Infrastructures
{
    public static class MessagingConfigureServices
    {
        public static void AddSMSMessaging(this IServiceCollection services)
        {
            services.AddHttpClient("SmsIr", c =>
            {
                c.BaseAddress = new Uri("http://restfulsms.com/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<ISmsSender, SmsSender>();
            services.AddScoped<ISmsTokenGenerator, SmsTokenGenerator>();
            services.AddScoped<ISMSTemplateDataConverter, SMSTemplateDataConverter>();
            services.AddMemoryCache();

        }
    }
}
