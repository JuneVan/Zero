namespace Zero.Threading
{
    public class NullThreadSignal : IThreadSignal
    {
        public CancellationToken Token => CancellationToken.None;
    }
}
