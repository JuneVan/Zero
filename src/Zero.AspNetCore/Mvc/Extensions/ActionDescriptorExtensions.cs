namespace Zero.AspNetCore.Mvc.Extensions
{
    public static class ActionDescriptorExtensions
    {
        public static bool IsControllerAction(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor is ControllerActionDescriptor;
        }
        public static ControllerActionDescriptor AsControllerActionDescriptor(this ActionDescriptor actionDescriptor)
        {
            if (!actionDescriptor.IsControllerAction())
            {
                throw new ZeroException($"变量`{nameof(actionDescriptor)}`非{typeof(ControllerActionDescriptor).AssemblyQualifiedName}类型");
            }
            return (ControllerActionDescriptor)actionDescriptor;
        }
        public static MethodInfo GetMethodInfo(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.AsControllerActionDescriptor().MethodInfo;
        }
    }
}
