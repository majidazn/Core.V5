using Microsoft.AspNetCore.Http;
using System;

namespace Framework.Extensions
{
    public static class UrlBuilder
    {
        public static string BuildUrl(HttpContext httpContext)
        {
            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = httpContext.Request.Scheme;
            uriBuilder.Host = httpContext.Request.Host.Host;
            uriBuilder.Path = httpContext.Request.Path.ToString();
            uriBuilder.Query = httpContext.Request.QueryString.ToString();

            return uriBuilder.Uri.AbsolutePath;
        }
    }
}
