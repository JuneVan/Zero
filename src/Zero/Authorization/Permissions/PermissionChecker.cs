namespace Zero.Authorization.Permissions
{
    public class PermissionChecker : IPermissionChecker
    {
        private readonly IIdentifier _identifier;
        private readonly IPermissionStore _permissionStore;
        public PermissionChecker(IIdentifier identifier,
          IPermissionStore permissionStore)
        {
            _identifier = identifier;
            _permissionStore = permissionStore;
        }
        public virtual async Task<bool> AuthorizeAsync(string permissionName)
        {
            if (permissionName.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(permissionName), "权限名不能为空。");
            if (!_identifier.UserId.HasValue)
                throw new AuthorizationException("用户未登录");

            var permissions = await _permissionStore.GetOrCreatePermissionsAsync(_identifier.UserId.Value);
            if (permissions == null || !permissions.Contains(permissionName))
                return false;
            return true;
        }
    }
}
