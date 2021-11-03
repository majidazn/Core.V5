using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Exceptions
{
    public class ServiceInterceptor : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
           ActionExecutingContext context,
           ActionExecutionDelegate next)
        {
            // do something before
            var resultContext = await next();
            // do something after the action executes
        }
    }
}
