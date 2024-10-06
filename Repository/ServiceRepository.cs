using MongoDB.Driver;
using Reservas.Data;
using Reservas.Models;
using Reservas.Repository.Contract;

namespace Reservas.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IMongoCollection<Service> _service;

        public ServiceRepository(MongoDbContext context)
        {
            _service = context.Services;
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _service.Find(service => true).ToListAsync();
        }

        public async Task<Service> GetByIdAsync(string id_service)
        {
            return await _service.Find<Service>(service => service.Id_Service == id_service).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Service service)

        {
            await _service.InsertOneAsync(service);
        }

        public async Task UpdateAsync(string id_service, Service service)
        {
            await _service.ReplaceOneAsync(service => service.Id_Service == id_service, service);
        }

        public async Task DeleteAsync(string id_service)
        {
            await _service.DeleteOneAsync(service => id_service == service.Id_Service);
        }

    }
}
