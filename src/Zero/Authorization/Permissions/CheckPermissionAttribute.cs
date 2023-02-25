namespace Zero.Authorization.Permissions
{
    /// <summary>
    /// 检查权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CheckPermissionAttribute : Attribute
    {
        public CheckPermissionAttribute(params string[] permissionNames)
        {
            if (permissionNames == null) throw new ArgumentNullException(nameof(permissionNames), $"权限名不能为空。");
            PermissionNames = permissionNames;
        }
        public string[] PermissionNames { get; set; }
    }
}
