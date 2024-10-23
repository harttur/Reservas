namespace Reservas.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public string Name { get; set; }

        public LoginResponse(string token, DateTime expiration, string name) 
        
        {
            Token = token;
            Expiration = expiration;
            Name = name;
        }

    }
}
