namespace Zero.AspNetCore.Mvc.Authorization
{
    /// <summary>
    /// 提供权限检查过滤器
    /// </summary>
    public class PermissionFilter : IAsyncAuthorizationFilter, IOrderedFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IIdentifier _identifier;
        public PermissionFilter(ILoggerFactory loggerFactory,
            IIdentifier identifier)
        {
            _loggerFactory = loggerFactory;
            _identifier = identifier;
        }

        public int Order => 2;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor.AsControllerActionDescriptor();
            var logger = _loggerFactory.CreateLogger(actionDescriptor.ActionName);
            try
            {
                Endpoint endpoint = context?.HttpContext?.GetEndpoint();
                if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
                    return;
                if (!context.ActionDescriptor.IsControllerAction())
                    return;

                // 未设置Authorize特性时，无需验证权限
                var authorizeAttributes = ReflectionHelper.GetAttributes<AuthorizeAttribute>(actionDescriptor.ControllerTypeInfo, actionDescriptor.MethodInfo);
                if (authorizeAttributes == null || !authorizeAttributes.Any())
                    return;

                // 无需检查权限
                var noCheckPermissionAttributes = ReflectionHelper.GetAttributes<NoCheckPermissionAttribute>(actionDescriptor.ControllerTypeInfo, actionDescriptor.MethodInfo);
                if (authorizeAttributes != null && authorizeAttributes.Any())
                    return;

                // 检查权限配置
                var permissionAttributes = ReflectionHelper.GetAttributes<CheckPermissionAttribute>(actionDescriptor.ControllerTypeInfo, actionDescriptor.MethodInfo);
                if (permissionAttributes == null || !permissionAttributes.Any())
                    return;
                var permissionChecker = context.HttpContext.RequestServices.GetRequiredService<IPermissionChecker>();
                foreach (var permissionAttribute in permissionAttributes)
                {
                    foreach (var permissionName in permissionAttribute.PermissionNames)
                    {
                        if (!await permissionChecker.AuthorizeAsync(permissionName))
                        {
                            logger.LogDebug($"用户[{_identifier.UserId}]操作访问被拒绝，权限值`{permissionName}`。");
                            if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                            {
                                context.Result = new ObjectResult(new AjaxResponse(false, $"您没有权限进行此操作，权限值`{permissionName}`。"))
                                {
                                    StatusCode = (int)HttpStatusCode.Forbidden
                                };
                            }
                            else
                            {
                                context.Result = new ForbidResult();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exceptionInfo = ExceptionConverter.ConvertToExceptionInfo(ex, context.HttpContext);
                var severity = (exceptionInfo.Exception as IHasLogLevel)?.Level ?? LogLevel.Error;
                logger.Log(severity, ex, ex.Message);
                context.HttpContext.Response.StatusCode = exceptionInfo.StatusCode;
                var ajaxResponse = new AjaxResponse(false, exceptionInfo.Exception.Message);
                context.Result = new ObjectResult(ajaxResponse);
            }
        }
    }
}
