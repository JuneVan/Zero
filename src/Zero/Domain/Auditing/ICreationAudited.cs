namespace Zero.Domain.Auditing
{
    public interface ICreationAudited : IHasCreationTime
    {
        int CreatorUserId { get; set; }
    }
}