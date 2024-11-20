using Reservas.Dtos;
using Reservas.Mappers;
using Reservas.Models;
using Reservas.Repository.Contract;
using Reservas.Services.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Reservas.Services
{
	public class ServiceService : IServiceService
	{
		private readonly IServiceRepository _serviceRepository;
		private readonly ILogger<ServiceService> _logger;
        private readonly AppDbContext _dbContext;

        // Construtor com injeção de dependências
        public ServiceService(IServiceRepository serviceRepository, ILogger<ServiceService> logger)
		{
			_serviceRepository = serviceRepository;
			_logger = logger;
		}

		// Método privado para buscar o serviço ou lançar exceção caso não encontrado
		private async Task<Service> GetServiceOrThrow(string id_Service)
		{
			var service = await _serviceRepository.GetByIdAsync(id_Service);
			if (service == null)
			{
				throw new KeyNotFoundException($"Service with ID {id_Service} not found.");
			}
			return service;
		}

		// Método para criar um novo serviço
		public async Task<Service> CreateServiceAsync(ServiceDto serviceDto)
		{
			if (serviceDto == null)
			{
				throw new ArgumentNullException(nameof(serviceDto), "Service DTO cannot be null.");
			}

			var service = ServiceMapper.ToEntity(serviceDto);
			await _serviceRepository.CreateAsync(service);

			_logger.LogInformation($"Service created with ID {service.Id_Service}.");

			return service;
		}

		// Método para deletar um serviço
		public async Task DeleteServiceAsync(string id_Service)
		{
			var service = await GetServiceOrThrow(id_Service);

			await _serviceRepository.DeleteAsync(id_Service);
			_logger.LogInformation($"Service with ID {id_Service} has been deleted.");
		}

		// Método para obter todos os serviços
		public async Task<List<ServiceDto>> GetAllServicesAsync()
		{
			var services = await _serviceRepository.GetAllAsync();
			_logger.LogInformation($"Fetched {services.Count} services.");

			return ServiceMapper.ToDtoList(services);
		}

		// Método para buscar um serviço por ID
		public async Task<ServiceDto?> GetServiceByIdAsync(string id_Service)
		{
			var service = await _serviceRepository.GetByIdAsync(id_Service);
			if (service == null)
			{
				return null; // Retorna null se não encontrar
			}

			return ServiceMapper.ToDto(service);
		}

		// Método para atualizar um serviço
		public async Task<ServiceDto> UpdateServiceAsync(string id_Service, ServiceDto serviceDto)
		{
			if (serviceDto == null)
			{
				throw new ArgumentNullException(nameof(serviceDto), "Service DTO cannot be null.");
			}

			var service = await GetServiceOrThrow(id_Service);

			var updatedService = ServiceMapper.ToEntity(serviceDto);
			await _serviceRepository.UpdateAsync(id_Service, updatedService);

			_logger.LogInformation($"Service with ID {id_Service} has been updated.");

			return serviceDto;
		}

        public ServiceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Service>> GetAllServicesAsync(CancellationToken cancellationToken)
        {
            // Recupera todos os serviços do banco de dados
            return await _dbContext.Services
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

    }
}
