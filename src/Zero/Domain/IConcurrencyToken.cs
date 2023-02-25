namespace Zero.Domain
{
    public interface IConcurrencyToken
    {
        string ConcurrencyStamp { get; set; }
    }
}
