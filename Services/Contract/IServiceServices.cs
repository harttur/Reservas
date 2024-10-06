using Reservas.Dtos;

namespace Reservas.Services.Contract
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetAllServicesAsync();
        Task<ServiceDto> GetServiceByIdAsync(string id_service);
        Task CreateServiceAsync(ServiceDto Service);
        Task UpdateServiceAsync(string Id_Service, ServiceDto ServicesDto);
        Task DeleteServiceAsync(string Id_Service);
    }
}
