namespace Zero.Authorization.Permissions
{
    public interface IPermissionStore
    {
        Task<IList<string>> GetOrCreatePermissionsAsync(int userId);
        Task ClearPermissionsAsync(int userId);
    }
}
