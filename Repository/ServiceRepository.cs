using MongoDB.Driver;
using Reservas.Data;
using Reservas.Models;
using Reservas.Repository.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace Reservas.Repository
{
	public class ServiceRepository : IServiceRepository
	{
		private readonly IMongoCollection<Service> _service;

		public ServiceRepository(MongoDbContext context)
		{
			_service = context.Services;
		}

		/// <summary>
		/// Recupera todos os serviços.
		/// </summary>
		public async Task<List<Service>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _service.Find(Builders<Service>.Filter.Empty)
								  .ToListAsync(cancellationToken);
		}

		/// <summary>
		/// Recupera um serviço pelo ID.
		/// </summary>
		public async Task<Service> GetByIdAsync(string id_service, CancellationToken cancellationToken = default)
		{
			var filter = Builders<Service>.Filter.Eq(s => s.Id_Service, id_service);
			return await _service.Find(filter).FirstOrDefaultAsync(cancellationToken);
		}

		/// <summary>
		/// Cria um novo serviço.
		/// </summary>
		public async Task CreateAsync(Service service, CancellationToken cancellationToken = default)
		{
			await _service.InsertOneAsync(service, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Atualiza um serviço existente.
		/// </summary>
		public async Task UpdateAsync(string id_service, Service service, CancellationToken cancellationToken = default)
		{
			var filter = Builders<Service>.Filter.Eq(s => s.Id_Service, id_service);
			await _service.ReplaceOneAsync(filter, service, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Deleta um serviço pelo ID.
		/// </summary>
		public async Task DeleteAsync(string id_service, CancellationToken cancellationToken = default)
		{
			var filter = Builders<Service>.Filter.Eq(s => s.Id_Service, id_service);
			await _service.DeleteOneAsync(filter, cancellationToken);
		}
	}
}
