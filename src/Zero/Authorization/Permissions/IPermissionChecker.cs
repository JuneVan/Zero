namespace Zero.Authorization.Permissions
{
    public interface IPermissionChecker
    {
        Task<bool> AuthorizeAsync(string permissionName);
    }
}
