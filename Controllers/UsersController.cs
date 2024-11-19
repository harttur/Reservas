using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservas.Dtos;
using Reservas.Models;
using Reservas.Services.Contract;

namespace Reservas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<List<UserDto>>> GetAllUsers()
		{
			var users = await _userService.GetAllUsersAsync();
			return Ok(users); // Retorna status 200 com os usuários
		}

		[Authorize]
		[HttpGet("{id_user}")]
		public async Task<ActionResult<UserDto>> GetUserById(string id_user)
		{
			var user = await _userService.GetUserByIdAsync(id_user);

			if (user == null)
			{
				return NotFound($"Usuário com ID {id_user} não encontrado."); // Mensagem mais explicativa
			}

			return Ok(user); // Retorna status 200 com o usuário encontrado
		}

		[HttpPost]
		public async Task<ActionResult> CreateUser([FromBody] UserDto userDto)
		{
			if (userDto == null)
			{
				return BadRequest("Dados de usuário não podem ser nulos.");
			}

			User user = await _userService.CreateUserAsync(userDto);

			return CreatedAtAction(nameof(GetUserById), new { id_user = user.Id_user }, userDto); // Retorna 201 Created
		}

		[Authorize]
		[HttpPut("{id_user}")]
		public async Task<ActionResult> UpdateUser(string id_user, UserDto userDto)
		{
			await _userService.UpdateUserAsync(id_user, userDto);
			return NoContent(); // Retorna 204 No Content
		}

		[Authorize]
		[HttpDelete("{id_user}")]
		public async Task<ActionResult> DeleteUser(string id_user)
		{
			var user = await _userService.GetUserByIdAsync(id_user);
			if (user == null)
			{
				return NotFound($"Usuário com ID {id_user} não encontrado."); // Verificação antes de deletar
			}

			await _userService.DeleteUserAsync(id_user);
			return NoContent(); // Retorna 204 No Content após exclusão bem-sucedida
		}
	}
}
