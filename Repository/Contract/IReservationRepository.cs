using Reservas.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Reservas.Repository.Contract
{
	/// <summary>
	/// Interface que define os métodos assíncronos para manipulação de reservas.
	/// </summary>
	public interface IReservationRepository
	{
		/// <summary>
		/// Recupera todas as reservas.
		/// </summary>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task<List<Reservation>> GetAllAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Recupera uma reserva pelo ID.
		/// </summary>
		/// <param name="idReservation">ID da reserva.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task<Reservation> GetByIdAsync(string idReservation, CancellationToken cancellationToken = default);

		/// <summary>
		/// Cria uma nova reserva.
		/// </summary>
		/// <param name="entity">A reserva a ser criada.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task CreateAsync(Reservation entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Atualiza uma reserva existente.
		/// </summary>
		/// <param name="idReservation">ID da reserva a ser atualizada.</param>
		/// <param name="entity">Os novos dados da reserva.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task UpdateAsync(string idReservation, Reservation entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deleta uma reserva.
		/// </summary>
		/// <param name="idReservation">ID da reserva a ser deletada.</param>
		/// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
		Task DeleteAsync(string idReservation, CancellationToken cancellationToken = default);
	}
}
