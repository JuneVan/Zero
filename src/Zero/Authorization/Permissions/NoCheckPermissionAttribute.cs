namespace Zero.Authorization.Permissions
{
    /// <summary>
    /// 无需检查权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoCheckPermissionAttribute : Attribute
    {
    }
}
