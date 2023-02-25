namespace Zero.Threading
{
    public interface IThreadSignal
    {
        CancellationToken Token { get; }
    }
}
