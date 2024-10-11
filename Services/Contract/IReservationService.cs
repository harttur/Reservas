using Reservas.Dtos;
using Reservas.Models;

namespace Reservas.Services.Contract
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllReservationAsync();
        Task<ReservationDto> GetReservationByIdAsync(string id_reservation);
        Task <Reservation>CreateReservationAsync(ReservationDto reservation);
        Task UpdateReservationAsync(string id_reservation, ReservationDto reservationDto);
        Task DeleteReservationAsync(string id_reservation);

    }
}
