namespace Zero.Domain.Auditing
{
    public interface IAudited : ICreationAudited, IModificationAudited
    {
    }
}