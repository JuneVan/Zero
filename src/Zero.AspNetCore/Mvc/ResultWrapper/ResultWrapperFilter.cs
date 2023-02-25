namespace Zero.AspNetCore.Mvc.ResultWrapper
{
    /// <summary>
    /// 提供结果格式化结果过滤器
    /// </summary>
    public class ResultWrapperFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
                return;
            var wrapResultAttribute = ReflectionHelper.GetAttributeOrDefault(
                 context.ActionDescriptor.GetMethodInfo(), WrapResultAttribute.DefaultWrapResult);
            if (!wrapResultAttribute.Enable)
                return;
            BuildResultWrapping(context);
        }
        private static void BuildResultWrapping(FilterContext context)
        {
            var resultExecutingContext = context as ResultExecutingContext;
            var actionResult = resultExecutingContext?.Result;
            switch (actionResult)
            {
                case ObjectResult objectResult:
                    objectResult.Value = new AjaxResponse(objectResult.Value);
                    objectResult.DeclaredType = typeof(AjaxResponse);
                    break;
                case JsonResult jsonResult:
                    jsonResult.Value = new AjaxResponse(jsonResult.Value);
                    break;
                case EmptyResult:
                    if (resultExecutingContext != null)
                        resultExecutingContext.Result = new ObjectResult(new AjaxResponse());
                    break;
            }
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
