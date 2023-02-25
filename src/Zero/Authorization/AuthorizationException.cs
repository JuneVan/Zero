namespace Zero.Authorization
{
    public class AuthorizationException : ZeroException, IHasLogLevel
    {
        public AuthorizationException(string message) : base(message)
        {

        }
        public AuthorizationException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public LogLevel Level => LogLevel.Warning;
    }
}
