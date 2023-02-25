namespace Zero.AspNetCore.JwtTokens
{
    public sealed class JwtTokenBuilder
    {
        private SecurityKey _securityKey = null;
        private bool _useissuer;
        private string _issuer = "";
        private bool _useaudience;
        private string _audience = "";
        private readonly List<Claim> _claims = new List<Claim>();
        private int _expiryInMinutes = 5;

        private void EnsureArguments()
        {
            if (_securityKey == null)
                throw new ArgumentNullException(nameof(_securityKey), "Jwt私钥不能为空。");

            if (_useissuer && string.IsNullOrEmpty(_issuer))
                throw new ArgumentNullException(nameof(_issuer), "Jwt Issuer不能为空。");

            if (_useaudience && string.IsNullOrEmpty(_audience))
                throw new ArgumentNullException(nameof(_audience), "Jwt Audience不能为空。");
        }

        public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            _securityKey = securityKey;
            return this;
        }

        public JwtTokenBuilder AddIssuer(string issuer)
        {
            _issuer = issuer;
            _useissuer = true;
            return this;
        }

        public JwtTokenBuilder AddAudience(string audience)
        {
            _audience = audience;
            _useaudience = true;
            return this;
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            _claims.Add(new Claim(type, value));
            return this;
        }

        public JwtTokenBuilder AddClaims(List<Claim> claims)
        {
            _claims.AddRange(claims);
            return this;
        }

        public JwtTokenBuilder AddExpiry(int expiryInMinutes)
        {
            _expiryInMinutes = expiryInMinutes;
            return this;
        }

        public JwtToken Build()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(_claims);

            var token = new JwtSecurityToken(
                              issuer: _issuer,
                              audience: _audience,
                              claims: claims,
                              expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                              signingCredentials: new SigningCredentials(
                                                        _securityKey,
                                                        SecurityAlgorithms.HmacSha256));

            return new JwtToken(token);
        }

    }
}
