using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;

namespace Framework.HeaderPropagation
{
    public class ConfigureHeaderPropagation
    {
        private IAsyncPolicy<HttpResponseMessage> TimeoutPolicy()
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(100);
        }

        public IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

        public AsyncRetryPolicy<HttpResponseMessage> RetryPolicy()
        {
            return HttpPolicyExtensions
             .HandleTransientHttpError()
             .Or<TimeoutRejectedException>() // thrown by Polly's TimeoutPolicy if the inner call times out
             .WaitAndRetryAsync(new[]
                 {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                 }, (exception, timeSpan, retryCount, context) =>
                 {
                     // do something   
                 });
        }
    }
}
