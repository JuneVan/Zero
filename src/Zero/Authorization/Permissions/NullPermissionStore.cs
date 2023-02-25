namespace Zero.Authorization.Permissions
{
    public class NullPermissionStore : IPermissionStore
    {
        public Task ClearPermissionsAsync(int userId)
        {
            throw new NotImplementedException("未实现`IPermissionStore`接口。");
        }

        public Task<IList<string>> GetOrCreatePermissionsAsync(int userId)
        {
            throw new NotImplementedException("未实现`IPermissionStore`接口。");
        }
    }
}
