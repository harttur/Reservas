using MongoDB.Driver;
using Reservas.Data;
using Reservas.Models;

namespace Reservas.Repository
{
    public class ReservationRepository
    {
        private readonly IMongoCollection<Reservetion> _reservetion;

        public ReservationRepository(MongoDbContext context)
        {
            _reservetion = context.Reservetions;
        }

        public async Task<List<Reservetion>> GetAllAsync()
        {
            return await _reservetion.Find(reservetion => true).ToListAsync();
        }

        public async Task<Reservetion> GetByIdAsync(string id_reservetion)
        {
            return await _reservetion.Find<Reservetion>(reservetion => reservetion.Id_reservation == id_reservetion).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Reservetion reservetion)

        {
            await _reservetion.InsertOneAsync(reservetion);
        }

        public async Task UpdateAsync(string id_reservetion, Reservetion reservetion)
        {
            await _reservetion.ReplaceOneAsync(reservetion => reservetion.Id_reservation == id_reservetion, reservetion);
        }

        public async Task DeleteAsync(string id_reservetion)
        {
            await _reservetion.DeleteOneAsync(reservetion => id_reservetion == reservetion.Id_reservation);
        }
    }
}
