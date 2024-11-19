using Microsoft.AspNetCore.Mvc;
using Reservas.Dtos;
using Reservas.Services.Contract;
using Reservas.Services;

namespace Reservas.Controllers
{
	/// <summary>
	/// Controlador responsável pela autenticação e geração de tokens.
	/// </summary>
	[ApiController]
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		private readonly JwtTokenService _tokenService;
		private readonly IUserService _userService;  // Serviço de usuário para validar as credenciais

		/// <summary>
		/// Construtor que recebe o serviço de geração de tokens JWT e o serviço de usuário.
		/// </summary>
		/// <param name="tokenService">Serviço de geração de tokens JWT.</param>
		/// <param name="userService">Serviço de validação de usuário.</param>
		public AuthController(JwtTokenService tokenService, IUserService userService)
		{
			_tokenService = tokenService;
			_userService = userService;
		}

		/// <summary>
		/// Endpoint de login para gerar o token JWT.
		/// </summary>
		/// <param name="request">Dados do login.</param>
		/// <returns>Token JWT caso as credenciais sejam válidas.</returns>
		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginRequest request)
		{
			// Valida as credenciais usando um serviço de usuários
			var user = _userService.ValidateUser(request.Username, request.Password);

			if (user == null)
				return Unauthorized("Usuário ou senha inválidos.");

			// Gera o token caso as credenciais sejam válidas
			var token = _tokenService.GenerateToken(user.Id, user.Username);

			return Ok(new
			{
				Token = token,
				Expiration = DateTime.UtcNow.AddMinutes(30) // exemplo de tempo de expiração
			});
		}
	}
}
