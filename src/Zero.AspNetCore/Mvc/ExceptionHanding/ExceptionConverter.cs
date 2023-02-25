using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Zero.AspNetCore.Mvc.ExceptionHanding
{
    public static class ExceptionConverter
    {
        public static ExceptionInfo ConvertToExceptionInfo(Exception exception, HttpContext context)
        {
            if (exception is AggregateException aggregateException && aggregateException != null)
                exception = aggregateException;
            if (exception.InnerException != null)
                exception = exception.InnerException;

            int statusCode;
            if (exception is AuthorizationException)
            {
                statusCode = (context.User.Identity?.IsAuthenticated ?? false)
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }
            else if (exception is ValidationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is EntityNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }
            return new ExceptionInfo(exception, statusCode);
        }
    }
}
