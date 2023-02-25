namespace Zero.Domain.Auditing
{
    public interface IDeletionAudited : IHasDeletionTime
    {
        int? DeleterUserId { get; set; }
    }
}