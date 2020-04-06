using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using Aliera.Utilities.Logging.CustomExceptions;
using Enum = Aliera.Utilities.Logging.CustomExceptions.CustomErrorCodes;

namespace Aliera.Utilities.Logging.Middleware
{
    public static class MSSqlDbLog
    {
        public static void Invoke(LoggerSettings settings,
            HttpContext context, Exception ex, string messageTemplate,
            string message, double sw, int logLevel)
        {
            Logger logger = DbLog(settings, logLevel);

            switch (logLevel)
            {
                case (int)Enum.LogLevel.DebugModeRequest:
                    if (!string.IsNullOrEmpty(message))
                    {
                        logger.Debug(messageTemplate, context.Request.Path, context.Request.Method, message, sw);
                    }
                    else
                    {
                        logger.Debug(messageTemplate, context.Request.Path, context.Request.Method, sw);
                    }

                    break;
                case (int)Enum.LogLevel.DebugModeResponse:
                    logger.Debug(messageTemplate, context.Response.StatusCode, message, sw);
                    break;
                case (int)LogEventLevel.Fatal:
                    logger.Fatal(ex, messageTemplate, ex.Message, (int)CustomErrorCodes.Code.FatalErrorCode);
                    break;
                case (int)LogEventLevel.Verbose:
                    logger.Verbose(ex, messageTemplate, ex.Message, (int)CustomErrorCodes.Code.VerboseErrorCode);
                    break;
                case (int)LogEventLevel.Error:
                    logger.Error(ex, messageTemplate, context.Request.Method, context.Request.Path, 500, sw);
                    break;
                case (int)LogEventLevel.Information:
                    logger.Information(message);
                    break;
                case (int)LogEventLevel.Warning:
                    logger.Warning(message);
                    break;
                default:
                    logger.Debug(message);
                    break;
            }
        }

        public static void DbInvoke(LoggerSettings settings, Exception exception, string messageTemplate,
         int logLevel, params object[] propertyValues)
        {
            Logger logger = DbLog(settings, logLevel);
            switch (logLevel)
            {
                case (int)LogEventLevel.Debug:
                    logger.Debug(messageTemplate, propertyValues);
                    break;
                case (int)LogEventLevel.Fatal:
                    logger.Fatal(messageTemplate, propertyValues);
                    break;
                case (int)LogEventLevel.Verbose:
                    logger.Verbose(messageTemplate, propertyValues);
                    break;
                case (int)LogEventLevel.Error:
                    logger.Error(messageTemplate, propertyValues);
                    break;
                case (int)LogEventLevel.Information:
                    logger.Information(messageTemplate, propertyValues);
                    break;
                case (int)LogEventLevel.Warning:
                    logger.Warning(messageTemplate, propertyValues);
                    break;
                case (int)Enum.LogLevel.DebugModeError:
                    logger.Debug(exception, messageTemplate, propertyValues);
                    break;
                case (int)Enum.LogLevel.InformationModeError:
                    logger.Information(exception, messageTemplate, propertyValues);
                    break;
                case (int)Enum.LogLevel.VerboseModeError:
                    logger.Verbose(exception, messageTemplate, propertyValues);
                    break;
                case (int)Enum.LogLevel.FatalModeError:
                    logger.Fatal(exception, messageTemplate, propertyValues);
                    break;
                case (int)Enum.LogLevel.WarningModeError:
                    logger.Warning(exception, messageTemplate, propertyValues);
                    break;
                default:
                    break;
            }
        }

        private static Logger DbLog(LoggerSettings settings, int logLevel)
        {
            var levelSwitch = new LoggingLevelSwitch();

            switch (logLevel)
            {
                case (int)Enum.LogLevel.DebugModeRequest:
                case (int)Enum.LogLevel.DebugModeResponse:
                    levelSwitch.MinimumLevel = LogEventLevel.Debug;
                    break;
                case (int)LogEventLevel.Fatal:
                    levelSwitch.MinimumLevel = LogEventLevel.Fatal;
                    break;
                case (int)LogEventLevel.Verbose:
                    levelSwitch.MinimumLevel = LogEventLevel.Verbose;
                    break;
                case (int)LogEventLevel.Error:
                    levelSwitch.MinimumLevel = LogEventLevel.Error;
                    break;
                case (int)LogEventLevel.Information:
                    levelSwitch.MinimumLevel = LogEventLevel.Information;
                    break;
                case (int)LogEventLevel.Warning:
                    levelSwitch.MinimumLevel = LogEventLevel.Warning;
                    break;
                default:
                    levelSwitch.MinimumLevel = LogEventLevel.Debug;
                    break;
            }

            var logger = new LoggerConfiguration()
                 .MinimumLevel.ControlledBy(levelSwitch)
                                       .WriteTo.MSSqlServer(settings.ConnectionString,
                                        settings.TableName)
                                        .CreateLogger();
            return logger;
        }
    }
}
