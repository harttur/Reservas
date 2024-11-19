using Reservas.Dtos;
using Reservas.Models;

namespace Reservas.Services.Contract
{
	/// <summary>
	/// Interface para o serviço de usuários.
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Recupera todos os usuários.
		/// </summary>
		/// <returns>Uma lista de objetos UserDto.</returns>
		Task<List<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Recupera um usuário pelo ID.
		/// </summary>
		/// <param name="id_user">ID do usuário.</param>
		/// <returns>Um objeto UserDto com os detalhes do usuário.</returns>
		Task<UserDto> GetUserByIdAsync(string id_user, CancellationToken cancellationToken = default);

		/// <summary>
		/// Cria um novo usuário.
		/// </summary>
		/// <param name="userDto">Objeto UserDto contendo os dados do novo usuário.</param>
		/// <returns>O objeto UserDto do novo usuário criado.</returns>
		Task<UserDto> CreateUserAsync(UserDto userDto, CancellationToken cancellationToken = default);

		/// <summary>
		/// Atualiza os dados de um usuário existente.
		/// </summary>
		/// <param name="id_user">ID do usuário a ser atualizado.</param>
		/// <param name="userDto">Objeto UserDto contendo os dados atualizados do usuário.</param>
		Task UpdateUserAsync(string id_user, UserDto userDto, CancellationToken cancellationToken = default);

		/// <summary>
		/// Exclui um usuário pelo ID.
		/// </summary>
		/// <param name="id_user">ID do usuário a ser excluído.</param>
		Task DeleteUserAsync(string id_user, CancellationToken cancellationToken = default);

		/// <summary>
		/// Autentica o usuário baseado no nome de usuário e senha.
		/// </summary>
		/// <param name="username">Nome de usuário.</param>
		/// <param name="password">Senha do usuário.</param>
		/// <returns>O objeto UserDto ou User do usuário autenticado.</returns>
		Task<UserDto> Authenticate(string username, string password, CancellationToken cancellationToken = default);

		/// <summary>
		/// Gera um token JWT para o usuário.
		/// </summary>
		/// <param name="user">Objeto User contendo os dados do usuário.</param>
		/// <returns>Uma string contendo o token JWT gerado.</returns>
		string GenerateJwtToken(User user);
	}
}
