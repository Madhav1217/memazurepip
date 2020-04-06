namespace Aliera.Utilities.Logging.CustomExceptions
{
    public class CustomErrorCodes
    {
        public enum Code
        {
            FatalErrorCode = 1001,
            VerboseErrorCode = 1002
        }

        public enum LogLevel
        {            
            DebugModeResponse = 6,
            DebugModeRequest = 7,
            DebugModeError = 8,
            InformationModeError = 9,
            VerboseModeError = 10,
            FatalModeError = 11,
            WarningModeError = 12,
            Error = 12
        }
    }
}
