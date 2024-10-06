using MongoDB.Driver;
using Reservas.Data;
using Reservas.Models;
using Reservas.Repository.Contract;

namespace Reservas.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IMongoCollection<Reservation> _reservation;

        public ReservationRepository(MongoDbContext context)
        {
            _reservation = context.Reservations;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _reservation.Find(reservetion => true).ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(string id_reservetion)
        {
            return await _reservation.Find<Reservation>(reservetion => reservetion.Id_reservation == id_reservetion).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Reservation reservetion)

        {
            await _reservation.InsertOneAsync(reservetion);
        }

        public async Task UpdateAsync(string id_reservetion, Reservation reservetion)
        {
            await _reservation.ReplaceOneAsync(reservetion => reservetion.Id_reservation == id_reservetion, reservetion);
        }

        public async Task DeleteAsync(string id_reservetion)
        {
            await _reservation.DeleteOneAsync(reservetion => id_reservetion == reservetion.Id_reservation);
        }
    }
}
