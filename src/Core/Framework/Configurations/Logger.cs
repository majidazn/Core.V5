using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Framework.Configurations
{
    public static class Logger
    {
        public static void LoggerSetup(this ILoggerFactory loggerFactory, string filepath, string filename)
        {
            loggerFactory.AddFile($"{filepath}/{filename}.txt", LogLevel.Warning);
        }
    }
}
