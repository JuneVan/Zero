namespace Zero.AspNetCore.Threading
{
    public class HttpThreadSignal : IThreadSignal
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpThreadSignal(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public virtual CancellationToken Token => _httpContextAccessor?.HttpContext?.RequestAborted ?? CancellationToken.None;
    }
}
