using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SecurityHeaders
{
    public static class XContentType
    {
        public static void Configurations(IApplicationBuilder app)
        {
            app.UseXContentTypeOptions();
        }
    }
}
