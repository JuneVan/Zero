namespace Zero.Domain.Auditing
{
    public interface IHasCreationTime
    {
        DateTime CreatedTime { get; set; }
    }
}