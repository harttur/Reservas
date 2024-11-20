using MongoDB.Driver.Core.Authentication;

namespace Reservas.Dtos
{
    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
