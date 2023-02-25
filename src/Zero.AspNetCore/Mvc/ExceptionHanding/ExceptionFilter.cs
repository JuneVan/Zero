namespace Zero.AspNetCore.Mvc.ExceptionHanding
{
    /// <summary>
    /// 提供异常记录处理异常过滤器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
                return;

            var exceptionInfo = ExceptionConverter.ConvertToExceptionInfo(context.Exception, context.HttpContext);
            // 日志记录
            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(context.ActionDescriptor.AsControllerActionDescriptor().ControllerName);
            var severity = (exceptionInfo.Exception as IHasLogLevel)?.Level ?? LogLevel.Error;
            logger.Log(severity, exceptionInfo.Exception.Message, exceptionInfo.Exception);

            // 返回状态
            context.HttpContext.Response.StatusCode = exceptionInfo.StatusCode;
            var ajaxResponse = new AjaxResponse(false, exceptionInfo.Exception.Message);
            context.Result = new ObjectResult(ajaxResponse);
            context.ExceptionHandled = true;
        }
    }
}
