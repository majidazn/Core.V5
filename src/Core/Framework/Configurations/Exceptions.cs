using Framework.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace Framework.Configurations
{
    public static class Exceptions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
} 
