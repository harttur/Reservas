namespace Reservas.Configurations
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public string Issuer { get; set; } 
        public string Audience { get; set; }    
        public int ExpirationMinutes { get; set; }

        public JwtConfiguration(string secret, string issuer, string audience, int experationMinutes) 
        {
            Secret = secret;
            Issuer = issuer;
            Audience = audience;
            ExpirationMinutes = experationMinutes;
        }
    }
}
