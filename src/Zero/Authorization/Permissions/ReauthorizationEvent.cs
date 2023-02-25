namespace Zero.Authorization.Permissions
{
    public class ReauthorizationEvent : INotification
    {
        public int UserId { get; set; }
    }
}
