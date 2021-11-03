using Core.Ocelot.Configurations;
using Core.Ocelot.Extensions;
using Core.Ocelot.LoadBalancerFactories;
using Core.Ocelot.Servers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Ocelot.Middlewares.IPRateLimiting
{
    public class LoadBalancerMiddleware
    {
        private RequestDelegate nextMiddleware;
        public LoadBalancerMiddleware(RequestDelegate next)
        {
            this.nextMiddleware = next;
        }

        public static void Log(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!System.IO.File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = System.IO.File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        public async Task Invoke(HttpContext context)
        {
            ILoadBalancerFactory loadBalancerFactory = null;
            IHttpClientFactory httpClientFactory = null;
            Server server = null;
            HttpResponseMessage response = null;

            try
            {
                if (context == null) return;


                loadBalancerFactory = (ILoadBalancerFactory)context.RequestServices.GetService(typeof(ILoadBalancerFactory));
                httpClientFactory = (IHttpClientFactory)context.RequestServices.GetService(typeof(IHttpClientFactory));

                server = loadBalancerFactory.Get();

                var ip = string.Empty;
                if (string.IsNullOrWhiteSpace(server.Port))
                    ip = server.IP;
                else
                    ip = server.IP + ":" + server.Port;

                var path = context.Request.Path;


                var httpClient = httpClientFactory.CreateClient(ip);
           
        
            //System.Diagnostics.Debugger.Break();
            
                ThreadLocal<Stopwatch> _localStopwatch = new ThreadLocal<Stopwatch>(() => new Stopwatch());
                _localStopwatch.Value.Reset();
                _localStopwatch.Value.Start();

                response = await ExecuteRequest(context, ip, path, httpClient, response);




                _localStopwatch.Value.Stop();
                server.FixedSizedQueues.Enqueue(_localStopwatch.Value.ElapsedMilliseconds);
                return;
            }
            catch (HttpRequestException ex)
            {
                Log("response.StatusCode=" + response.StatusCode + " , Exception=" + ex.Message.ToString());

                loadBalancerFactory.IncreaseFailedCount(server);

                response.EnsureSuccessStatusCode();
                await PrepareResponse(context, response);
            }


            await this.nextMiddleware.Invoke(context);
        }

        private static async Task<HttpResponseMessage> ExecuteRequest(HttpContext context, string ip, PathString path, HttpClient httpClient, HttpResponseMessage response)
        {
            string url = MakeUrl(context, ip, path);

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(context.Request.Method), url);
            request.Content = await context.Request.MapContent();

            httpClient.AddAuthorizationHeaderIfExistsOnRequest(context);

            response = httpClient.SendAsync(request).GetAwaiter().GetResult();

            if (!response.ReasonPhrase.ToLower().Contains("bad request"))
                response.EnsureSuccessStatusCode();

            await PrepareResponse(context, response);


            return response;
        }

        private static string MakeUrl(HttpContext context, string ip, PathString path)
        {
            var queryParams = ConvertQueryStringToDictionary(context.Request.QueryString.ToString());
            var url = QueryHelpers.AddQueryString(ip + path, queryParams);
            return url;
        }

        private static Dictionary<string, string> ConvertQueryStringToDictionary(string queryString)
        {
            var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers
                .ParseQuery(queryString);

            Dictionary<string, string> temp = new Dictionary<string, string>();
            foreach (var q in query)
            {
                temp.Add(q.Key, q.Value.ToString());
            }
            return temp;
        }

        private static async Task PrepareResponse(HttpContext context, HttpResponseMessage response)
        {
            try
            {
                if (response.Content.Headers.ContentLength != null)
                {
                    AddHeaderIfDoesntExist(context, new Header("Content-Length", new[] { response.Content.Headers.ContentLength.ToString() }));
                }

                var content = await response.Content.ReadAsStreamAsync();

                using (content)
                {
                    if (response.StatusCode != HttpStatusCode.NotModified && context.Response.ContentLength != 0)
                    {
                        await content.CopyToAsync(context.Response.Body);
                    }
                }
            }
            catch (Exception ex)
            {
                Log("PrepareResponse: response=" + response.StatusCode + " , Exception=" + ex.Message.ToString());
            }
       

            //context.Response.StatusCode = (int)response.StatusCode;
            //if (context.Response.StatusCode == 204)
            //{
            //    context.Response.ContentLength = 0;
            //}

            //context.Response.ContentType = new MediaTypeHeaderValue("application/json").MediaType;
        }

        private static void AddHeaderIfDoesntExist(HttpContext context, Header httpResponseHeader)
        {
            if (!context.Response.Headers.ContainsKey(httpResponseHeader.Key))
            {
                context.Response.Headers.Add(httpResponseHeader.Key, new StringValues(httpResponseHeader.Values.ToArray()));
            }
        }

        private async Task<byte[]> ToByteArray(Stream stream)
        {
            using (stream)
            {
                using (var memStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memStream);
                    return memStream.ToArray();
                }
            }
        }

    }
}
