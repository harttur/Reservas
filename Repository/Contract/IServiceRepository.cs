using Reservas.Models;

namespace Reservas.Repository.Contract
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(string Id_Service);
        Task CreateAsync(Service entity);
        Task UpdateAsync(string Id_Service, Service entity);
        Task DeleteAsync(string Id_Service);
    }

}
