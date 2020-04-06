using System;

namespace Aliera.Utilities.Logging.CustomExceptions
{
    public class VerboseException : Exception
    {
        public VerboseException()
        {

        }

        public VerboseException(string message) : base(message)
        {

        }

        public VerboseException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
