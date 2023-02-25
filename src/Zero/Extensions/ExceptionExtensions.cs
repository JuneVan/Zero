namespace Zero.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}