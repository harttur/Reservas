using Reservas.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Reservas.Repository.Contract
{
	/// <summary>
	/// Interface para operações CRUD relacionadas aos usuários.
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Recupera todos os usuários.
		/// </summary>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Recupera um usuário pelo ID.
		/// </summary>
		/// <param name="idUser">ID do usuário.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task<User> GetByIdAsync(string idUser, CancellationToken cancellationToken = default);

		/// <summary>
		/// Cria um novo usuário.
		/// </summary>
		/// <param name="entity">O usuário a ser criado.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task CreateAsync(User entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Atualiza um usuário existente.
		/// </summary>
		/// <param name="idUser">ID do usuário a ser atualizado.</param>
		/// <param name="entity">Os novos dados do usuário.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task UpdateAsync(string idUser, User entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deleta um usuário.
		/// </summary>
		/// <param name="idUser">ID do usuário a ser deletado.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task DeleteAsync(string idUser, CancellationToken cancellationToken = default);

		/// <summary>
		/// Recupera um usuário pelo nome de usuário.
		/// </summary>
		/// <param name="userName">Nome de usuário.</param>
		User GetUserByUsername(string userName);
	}
}
