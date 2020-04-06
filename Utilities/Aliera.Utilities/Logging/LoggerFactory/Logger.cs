using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;

namespace Aliera.Utilities.Logging.LoggerFactory
{
    public static class Logger
    {
        public static void InjectSerilLog(IServiceCollection services, IConfiguration Configuration)
        {
            services
                           .AddTransient(typeof(ICustomLoggerFactory), typeof(CustomLoggerFactory))
                            .AddSingleton<ILoggerFactory>(svc =>
                            {
                                var logger = new LoggerConfiguration()
                                    .MinimumLevel.Debug()
                                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                    .MinimumLevel.Override("System",LogEventLevel.Warning)
                                    .ReadFrom.Configuration(Configuration)                                   
                                    .CreateLogger();
                                Log.Logger = logger;

                                return new SerilogLoggerFactory(logger, true);
                            });
        }       
    }
}
