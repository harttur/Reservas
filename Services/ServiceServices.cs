using Reservas.Dtos;
using Reservas.Mappers;
using Reservas.Models;
using Reservas.Repository.Contract;
using Reservas.Services.Contract;

namespace Reservas.Services
{
    public class ServiceServices : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceServices(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task CreateServiceAsync(ServiceDto serviceDto)
        {
            var user = ServiceMapper.ToEntity(serviceDto);
            await _serviceRepository.CreateAsync(user);
        }

        public async Task DeleteServiceAsync(string id_Service)
        {
            var service = await _serviceRepository.GetByIdAsync(id_Service);
            if (service == null)
            {
                return;
            }
            await _serviceRepository.DeleteAsync(id_Service);
        }

        public async Task<List<ServiceDto>> GetAllServicesAsync()
        {
            var service = await _serviceRepository.GetAllAsync();
            return ServiceMapper.ToDtoList(service);
        }

        public async Task<ServiceDto> GetServiceByIdAsync(string id_Service)
        {
            var service = await _serviceRepository.GetByIdAsync(id_Service);
            if (service == null)
            {
                return null;
            }

            return ServiceMapper.ToDto(service);

        }

        public async Task UpdateServiceAsync(string id_Service, ServiceDto serviceDto)
        {
            var service = await _serviceRepository.GetByIdAsync(id_Service);
            if (service == null)
            {
                return;
            }

            var updatedService = ServiceMapper.ToEntity(serviceDto);
            await _serviceRepository.UpdateAsync(id_Service, updatedService);
        }
    }
}

