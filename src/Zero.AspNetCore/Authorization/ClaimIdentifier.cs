namespace Zero.AspNetCore.Authorization
{
    public class ClaimIdentifier : IIdentifier
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimIdentifier(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int? UserId
        {
            get
            {
                Claim userIdClaim = _httpContextAccessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                    return null;
                if (int.TryParse(userIdClaim.Value, out int userId))
                    return userId;
                return null;
            }
        }
    }
}
