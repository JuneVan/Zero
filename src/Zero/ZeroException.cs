namespace Zero
{
    public class ZeroException : Exception
    {
        public ZeroException(string message) : base(message)
        {

        }
        public ZeroException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
