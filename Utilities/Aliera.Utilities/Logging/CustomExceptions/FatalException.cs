using System;

namespace Aliera.Utilities.Logging.CustomExceptions
{
    public class FatalException : Exception
    {
        public FatalException()
        {

        }

        public FatalException(string message) : base(message)
        {

        }

        public FatalException(string message, Exception exception) : base(message, exception)
        {

        }

    }
}
