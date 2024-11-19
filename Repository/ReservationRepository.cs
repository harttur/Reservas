using MongoDB.Driver;
using Reservas.Data;
using Reservas.Models;
using Reservas.Repository.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace Reservas.Repository
{
	public class ReservationRepository : IReservationRepository
	{
		private readonly IMongoCollection<Reservation> _reservation;

		public ReservationRepository(MongoDbContext context)
		{
			_reservation = context.Reservations;
		}

		/// <summary>
		/// Recupera todas as reservas.
		/// </summary>
		public async Task<List<Reservation>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _reservation.Find(Builders<Reservation>.Filter.Empty)
									  .ToListAsync(cancellationToken);
		}

		/// <summary>
		/// Recupera uma reserva pelo ID.
		/// </summary>
		public async Task<Reservation> GetByIdAsync(string id_reservation, CancellationToken cancellationToken = default)
		{
			var filter = Builders<Reservation>.Filter.Eq(r => r.Id_reservation, id_reservation);
			return await _reservation.Find(filter).FirstOrDefaultAsync(cancellationToken);
		}

		/// <summary>
		/// Cria uma nova reserva.
		/// </summary>
		public async Task CreateAsync(Reservation reservation, CancellationToken cancellationToken = default)
		{
			await _reservation.InsertOneAsync(reservation, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Atualiza uma reserva existente.
		/// </summary>
		public async Task UpdateAsync(string id_reservation, Reservation reservation, CancellationToken cancellationToken = default)
		{
			var filter = Builders<Reservation>.Filter.Eq(r => r.Id_reservation, id_reservation);
			await _reservation.ReplaceOneAsync(filter, reservation, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Deleta uma reserva pelo ID.
		/// </summary>
		public async Task DeleteAsync(string id_reservation, CancellationToken cancellationToken = default)
		{
			var filter = Builders<Reservation>.Filter.Eq(r => r.Id_reservation, id_reservation);
			await _reservation.DeleteOneAsync(filter, cancellationToken);
		}
	}
}
