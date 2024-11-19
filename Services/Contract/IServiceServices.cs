using Reservas.Dtos;
using Reservas.Models;

namespace Reservas.Services.Contract
{
	/// <summary>
	/// Interface para os serviços de serviço.
	/// </summary>
	public interface IServiceService
	{
		/// <summary>
		/// Recupera todos os serviços.
		/// </summary>
		/// <returns>Uma lista de objetos ServiceDto.</returns>
		Task<List<ServiceDto>> GetAllServicesAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Recupera um serviço pelo ID.
		/// </summary>
		/// <param name="id_service">ID do serviço.</param>
		/// <returns>Um objeto ServiceDto com os detalhes do serviço.</returns>
		Task<ServiceDto> GetServiceByIdAsync(string id_service, CancellationToken cancellationToken = default);

		/// <summary>
		/// Cria um novo serviço.
		/// </summary>
		/// <param name="serviceDto">Objeto ServiceDto contendo os detalhes do novo serviço.</param>
		/// <returns>O objeto ServiceDto do novo serviço criado.</returns>
		Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto, CancellationToken cancellationToken = default);

		/// <summary>
		/// Atualiza os detalhes de um serviço existente.
		/// </summary>
		/// <param name="id_service">ID do serviço a ser atualizado.</param>
		/// <param name="serviceDto">Objeto ServiceDto contendo os dados atualizados do serviço.</param>
		Task<ServiceDto> UpdateServiceAsync(string id_service, ServiceDto serviceDto, CancellationToken cancellationToken = default);

		/// <summary>
		/// Exclui um serviço pelo ID.
		/// </summary>
		/// <param name="id_service">ID do serviço a ser excluído.</param>
		Task DeleteServiceAsync(string id_service, CancellationToken cancellationToken = default);
	}
}
