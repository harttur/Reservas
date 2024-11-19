using Reservas.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Reservas.Repository.Contract
{
	/// <summary>
	/// Interface que define os métodos assíncronos para manipulação de serviços.
	/// </summary>
	public interface IServiceRepository
	{
		/// <summary>
		/// Recupera todos os serviços.
		/// </summary>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task<List<Service>> GetAllAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Recupera um serviço pelo ID.
		/// </summary>
		/// <param name="idService">ID do serviço.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task<Service> GetByIdAsync(string idService, CancellationToken cancellationToken = default);

		/// <summary>
		/// Cria um novo serviço.
		/// </summary>
		/// <param name="entity">O serviço a ser criado.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task CreateAsync(Service entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Atualiza um serviço existente.
		/// </summary>
		/// <param name="idService">ID do serviço a ser atualizado.</param>
		/// <param name="entity">Os novos dados do serviço.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task UpdateAsync(string idService, Service entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deleta um serviço.
		/// </summary>
		/// <param name="idService">ID do serviço a ser deletado.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task DeleteAsync(string idService, CancellationToken cancellationToken = default);
	}
}
