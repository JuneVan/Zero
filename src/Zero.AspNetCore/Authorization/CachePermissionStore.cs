namespace Zero.AspNetCore.Authorization
{
    public class CachePermissionStore : IPermissionStore
    {
        private readonly IThreadSignal _signal;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _distributedCache;
        public CachePermissionStore(
             IThreadSignal signal,
             IHttpClientFactory httpClientFactory,
             IHttpContextAccessor httpContextAccessor,
             IDistributedCache distributedCache,
             IOptions<AuthorizationOptions> options)
        {
            _signal = signal;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _distributedCache = distributedCache;
            PermissionUrl = options?.Value?.PermissionUrl;
        }
        protected string PermissionUrl { get; private set; }
        public async Task<IList<string>> GetOrCreatePermissionsAsync(int userId)
        {
            var cacheKey = string.Format(AuthorizationDefaults.CacheKey, userId);
            return await _distributedCache.GetOrCreateAsync(cacheKey, async factory =>
            {
                if (PermissionUrl.IsNullOrEmpty())
                    throw new ArgumentNullException(nameof(PermissionUrl), $"用户权限列表接口地址不能为空。");
                using var client = _httpClientFactory.CreateClient();
                var requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{PermissionUrl}?UserId={userId}")
                };
                string bearerToken = _httpContextAccessor.HttpContext.Request.Headers.GetBearerToken();
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                var response = await client.SendWithRetryAsync(requestMessage, cancellationToken: _signal.Token, 3);
                using StreamReader reader = new(response.Content.ReadAsStream(), Encoding.UTF8);
                var result = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<List<string>>(result);
            });
        }
        public async Task ClearPermissionsAsync(int userId)
        {
            var cacheKey = string.Format(AuthorizationDefaults.CacheKey, userId);
            await _distributedCache.RemoveAsync(cacheKey);
        }
    }
}
