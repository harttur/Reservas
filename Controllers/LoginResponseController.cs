using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservas.Dtos;
using Reservas.Models;
using Reservas.Services.Contract;
using ZstdSharp.Unsafe;

namespace Reservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginResponseController : ControllerBase
    {
        public readonly IUserService _userService;

        public LoginResponseController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var user = _userService.Authenticate(request.name, request.password);
            if (user == null)
            {
                return Unauthorized();
            }
            
            var token = _userService.GenerateJwtToken(user);
            var expiration = DateTime.UtcNow.AddMinutes(180);

                var loginResponse = new LoginResponse(token, expiration, user.Name);

                return Ok(loginResponse);
            
        }
    }
}
