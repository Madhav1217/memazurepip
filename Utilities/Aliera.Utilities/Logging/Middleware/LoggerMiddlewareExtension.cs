using Microsoft.AspNetCore.Builder;

namespace Aliera.Utilities.Logging.Middleware
{
    public static class LoggerMiddlewareExtension
    {
        public static IApplicationBuilder UserSerilogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerMiddleware>();
        }
    }
}
