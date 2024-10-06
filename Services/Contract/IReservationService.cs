using Reservas.Dtos;

namespace Reservas.Services.Contract
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllReservationAsync();
        Task<ReservationDto> GetReservationByIdAsync(string id_reservation);
        Task CreateReservationAsync(ReservationDto reservation);
        Task UpdateReservationAsunc(string id_reservation, ReservationDto reservationDto);
        Task DeleteReservationAsync(string id_reservation);

    }
}
