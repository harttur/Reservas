using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservas.Dtos;
using Reservas.Services.Contract;

namespace Reservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost ("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var user = _userService.Authenticate(request.name, request.password);
            if (user == null)
                return Unauthorized();

            var token = _userService.GenerateJwtToken(user);
            var expiration = DateTime.UtcNow.AddMinutes(180);
            return Ok(new TokenResponseDto(token, expiration ));
        }
    }
}
