using Reservas.Dtos;
using Reservas.Models;

namespace Reservas.Services.Contract
{
	/// <summary>
	/// Interface para os serviços de reserva.
	/// </summary>
	public interface IReservationService
	{
		/// <summary>
		/// Recupera todas as reservas.
		/// </summary>
		/// <returns>Uma lista de objetos ReservationDto.</returns>
		Task<List<ReservationDto>> GetAllReservationAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Recupera uma reserva pelo ID.
		/// </summary>
		/// <param name="id_reservation">ID da reserva.</param>
		/// <returns>Um objeto ReservationDto com os detalhes da reserva.</returns>
		Task<ReservationDto> GetReservationByIdAsync(string id_reservation, CancellationToken cancellationToken = default);
		/// <summary>
		/// Cria uma nova reserva.
		/// </summary>
		/// <param name="reservationDto">Objeto ReservationDto contendo os detalhes d
	}
}