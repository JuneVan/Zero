namespace Zero.Authorization
{
    public class NullIdentifier : IIdentifier
    {
        public int? UserId => null;
    }
}
