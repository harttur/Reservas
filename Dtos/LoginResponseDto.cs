using Reservas.Models;
using System.Data;

namespace Reservas.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Name { get; set; }

        public LoginResponseDto(string token, DateTime expiration, string name)
        {
            Token = token;
            Expiration = expiration;
            Name = name;
        }

        public static LoginResponseDto FromLoginResponse(LoginResponse response)
        {
            return new LoginResponseDto(
                response.Token,
                response.Expiration,
                response.Name
            );
        }




    }
}
