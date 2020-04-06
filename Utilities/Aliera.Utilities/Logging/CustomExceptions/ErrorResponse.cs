namespace Aliera.Utilities.Logging.CustomExceptions
{
    public class BaseResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }

    public class ErrorResponse : BaseResponse
    {
        public string Description { get; set; }
        public string InnerException { get; set; }
    }

    public class FatalResponse : BaseResponse { }

    public class CustomResponse : BaseResponse
    {
        public new double ErrorCode { get; set; }
    }
}
