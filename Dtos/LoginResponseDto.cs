[Route("api/[controller]")]
[ApiController]
public class LoginResponseController : ControllerBase
{
	private readonly IUserService _userService;

	public LoginResponseController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
	{
		if (request == null)
		{
			return BadRequest("Dados de login não podem ser nulos.");
		}

		// Autentica o usuário e retorna o UserDto
		var user = await _userService.Authenticate(request.Username, request.Password);
		if (user == null)
		{
			return Unauthorized("Usuário ou senha inválidos.");
		}

		// Gera o token JWT
		var token = _userService.GenerateJwtToken(user);
		var expiration = DateTime.UtcNow.AddMinutes(180);

		// Retorna o LoginResponse com token e data de expiração
		var loginResponse = new LoginResponse(token, expiration, user);
		return Ok(loginResponse);
	}
}
