namespace Zero.AspNetCore.Http
{
    public static class HeadersExtensions
    {
        /// <summary>
        /// 获取请求头中的Bearer值
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static string GetBearerToken(this IHeaderDictionary headers)
        {
            string authorization = headers.Authorization;
            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var tokenValue = authorization["Bearer ".Length..].Trim();
                return tokenValue;
            }
            return null;
        }
    }
}
