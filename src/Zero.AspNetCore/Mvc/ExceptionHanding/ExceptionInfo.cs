namespace Zero.AspNetCore.Mvc.ExceptionHanding
{
    public class ExceptionInfo
    {
        public ExceptionInfo(Exception exception, int statusCode)
        {
            Exception = exception;
            StatusCode = statusCode;
        }

        public Exception Exception { get; set; }
        public int StatusCode { get; set; }
    }
}
