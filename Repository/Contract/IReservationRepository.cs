using Reservas.Models;

namespace Reservas.Repository.Contract
{
    public interface IReservationRepository
    {

        Task<List<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(string Id_reservation);
        Task CreateAsync(Reservation entity);
        Task UpdateAsync(string Id_reservation, Reservation entity);
        Task DeleteAsync(string Id_reservation);
    }
}
