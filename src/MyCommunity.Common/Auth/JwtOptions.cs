namespace MyCommunity.Common.Auth
{
    public class JwtOptions
    {
        public string SecretKeys { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
    }
}
