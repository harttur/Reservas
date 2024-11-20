using Reservas.Dtos;
using Reservas.Models;

namespace Reservas.Services.Contract
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetAllServicesAsync();
        Task<ServiceDto> GetServiceByIdAsync(string id_service);
        Task<Service> CreateServiceAsync(ServiceDto serviceDto);
        Task<ServiceDto> UpdateServiceAsync(string Id_Service, ServiceDto ServicesDto);
        Task DeleteServiceAsync(string Id_Service);
        Task<List<Service>> GetAllServicesAsync(CancellationToken cancellationToken);
    }
}
