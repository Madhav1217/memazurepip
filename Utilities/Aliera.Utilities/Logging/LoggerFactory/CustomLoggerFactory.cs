using Serilog;
using Serilog.Events;
using System;
using Enum = Aliera.Utilities.Logging.CustomExceptions.CustomErrorCodes;
using Aliera.Utilities.Logging.CustomExceptions;
using Aliera.Utilities.Logging.Middleware;

namespace Aliera.Utilities.Logging.LoggerFactory
{
    public class CustomLoggerFactory : ICustomLoggerFactory
    {
        private readonly LoggerSettings _loggerSettings;
        public CustomLoggerFactory(LoggerSettings loggerSettings)
        {
            _loggerSettings = loggerSettings;
        }
        private ILogger logger = Log.ForContext<CustomLoggerFactory>();

        private readonly string debugMessageTemplate = "{0} Debug Information Logged Date {1}";
        private readonly string infoMessageTemplate = "{0}  Information Logged Date {1}";
        private readonly string veboseMessageTemplate = "{0} Verbose Information Logged Date {1}";
        private readonly string fatalMessageTemplate = "{0} Fatal Information Logged Date {1}";
        //private readonly string errorMessageTemplate = "{0} Error Information Logged Date {1}";
        private readonly string warningMessageTemplate = "{0} Warning Information Logged Date {1}";        

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(level, messageTemplate, propertyValues);
        }
        public void Debug(string message)
        {
            string debugInfo = string.Format(debugMessageTemplate, message, DateTime.UtcNow);
            if (_loggerSettings.Default.Equals(LogEventLevel.Debug.ToString())){                 
                 logger.Debug(debugInfo);
            }                
            
            if (_loggerSettings.IsSqlServerLog &&
                _loggerSettings.Default.Equals(LogEventLevel.Debug.ToString()))
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, debugInfo, (int)LogEventLevel.Debug, null);
            }
        }
        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            if (_loggerSettings.Default.Equals(LogEventLevel.Debug.ToString()))
            {
                logger.Debug(messageTemplate, propertyValues);
            }

            if (_loggerSettings.Default.Equals(LogEventLevel.Debug.ToString()) &&
                _loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, messageTemplate, (int)LogEventLevel.Debug, propertyValues);
            }
        }
        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            if (_loggerSettings.Default.Equals(LogEventLevel.Debug.ToString())){
                logger.Debug(exception, messageTemplate, propertyValues);
            }

            if (_loggerSettings.Default.Equals(LogEventLevel.Debug.ToString()) &&
               _loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, exception, messageTemplate, (int)Enum.LogLevel.DebugModeError, propertyValues);
            }
        }
        public void Information(string message)
        {
            string informationMessage = string.Format(infoMessageTemplate, message, DateTime.UtcNow);
            logger.Information(informationMessage);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, informationMessage, (int)LogEventLevel.Information, null);
            }
        }
        public void Information(string messageTemplate, params object[] propertyValues)
        {
            logger.Information(messageTemplate, propertyValues);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, messageTemplate, (int)LogEventLevel.Information, propertyValues);
            }
        }
        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Information(exception, messageTemplate, propertyValues);

            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, exception, messageTemplate, (int)Enum.LogLevel.InformationModeError, propertyValues);
            }
        }
        public void Warning(string message)
        {
            string warningMessage = string.Format(warningMessageTemplate, message, DateTime.UtcNow);
            logger.Information(warningMessage);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, warningMessage, (int)LogEventLevel.Warning, null);                
            }
        }
        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            logger.Warning(messageTemplate, propertyValues);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, messageTemplate, (int)LogEventLevel.Warning, propertyValues);
            }
        }
        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Warning(exception, messageTemplate, propertyValues);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, exception, messageTemplate, (int)Enum.LogLevel.WarningModeError, propertyValues);
            }
        }                
        public void Verbose(string message)
        {
            string veboseExceptionInformation = string.Format(veboseMessageTemplate, message, DateTime.UtcNow);
            if (_loggerSettings.Default.Equals(LogEventLevel.Debug.ToString()))
            {
                logger.Debug(veboseExceptionInformation);
            }

            if (_loggerSettings.IsSqlServerLog &&
                _loggerSettings.Default.Equals(LogEventLevel.Debug.ToString()))
            {                
                MSSqlDbLog.DbInvoke(_loggerSettings, null, veboseExceptionInformation, (int)LogEventLevel.Verbose, null);
            }
        }
        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            logger.Verbose(messageTemplate, propertyValues);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, messageTemplate, (int)LogEventLevel.Verbose, propertyValues);
            }
        }
        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Verbose(exception, messageTemplate, propertyValues);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, exception, messageTemplate, (int)Enum.LogLevel.VerboseModeError, propertyValues);
            }
        }
        public void Fatal(string message)
        {
            string fatalMessageInformation = string.Format(fatalMessageTemplate, message, DateTime.UtcNow);
            logger.Fatal(fatalMessageInformation);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, fatalMessageInformation, (int)LogEventLevel.Fatal, null);
            }
        }
        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            logger.Fatal(messageTemplate, propertyValues);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, messageTemplate, (int)LogEventLevel.Fatal, propertyValues);
            }
        }
        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Fatal(exception, messageTemplate, propertyValues);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, exception, messageTemplate, (int)Enum.LogLevel.FatalModeError, propertyValues);
            }
        }
        public void Error(string message)
        {
            string errorMessageTemplate = string.Format(fatalMessageTemplate, message, DateTime.UtcNow);
            logger.Error(errorMessageTemplate);
            if (_loggerSettings.IsSqlServerLog)
            {                
                MSSqlDbLog.DbInvoke(_loggerSettings, null, errorMessageTemplate, (int)LogEventLevel.Error, null);
            }
        }
        public void Error(string messageTemplate, params object[] propertyValues)
        {
            logger.Error(messageTemplate, propertyValues);
            if (_loggerSettings.IsSqlServerLog)
            {
                MSSqlDbLog.DbInvoke(_loggerSettings, null, messageTemplate, (int)LogEventLevel.Error, propertyValues);
            }
        }
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Error(exception, messageTemplate, propertyValues);
            if (_loggerSettings.IsSqlServerLog)
                MSSqlDbLog.DbInvoke(_loggerSettings, exception, messageTemplate,(int)LogEventLevel.Error, propertyValues);
        }

    }
}
