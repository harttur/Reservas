using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Reservas.Dtos;
using Reservas.Models;
using Reservas.Services;
using Reservas.Services.Contract;

namespace Reservas.Controllers
{
	[Route("api/service")]
	[ApiController]
	public class ServiceController : ControllerBase
	{
		private readonly IServiceService _serviceService;

		public ServiceController(IServiceService serviceService) // Corrigido nome do parâmetro
		{
			_serviceService = serviceService;
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<List<ServiceDto>>> GetAllServices()
		{
			var services = await _serviceService.GetAllServicesAsync(); // Corrigido nome da variável
			return Ok(services); // Retorna status 200 com os serviços
		}

		[HttpPost]
		public async Task<ActionResult> CreateService([FromBody] ServiceDto serviceDto)
		{
			if (serviceDto == null)
			{
				return BadRequest("Os dados do serviço não podem ser nulos.");
			}

			Service service = await _serviceService.CreateServiceAsync(serviceDto);

			return CreatedAtAction(nameof(GetServiceById),
								   new { id_service = service.Id_Service },
								   serviceDto); // Retorna 201 com a URL do novo recurso
		}

		[Authorize]
		[HttpGet("{id_service}")]
		public async Task<ActionResult<ServiceDto>> GetServiceById(string id_service)
		{
			var service = await _serviceService.GetServiceByIdAsync(id_service);

			if (service == null)
			{
				return NotFound($"Serviço com ID {id_service} não encontrado.");
			}

			return Ok(service); // Retorna o serviço encontrado
		}

		[Authorize]
		[HttpPut("{id_service}")]
		public async Task<IActionResult> UpdateServiceAsync(string id_service, [FromBody] ServiceDto serviceDto)
		{
			if (serviceDto == null)
			{
				return BadRequest("Os dados do serviço não podem ser nulos.");
			}

			var updatedService = await _serviceService.UpdateServiceAsync(id_service, serviceDto);

			if (updatedService == null)
			{
				return NotFound($"Serviço com ID {id_service} não encontrado.");
			}

			return Ok(updatedService); // Retorna o serviço atualizado com status 200
		}

		[Authorize]
		[HttpDelete("{id_service}")]
		public async Task<ActionResult> DeleteService(string id_service)
		{
			var service = await _serviceService.GetServiceByIdAsync(id_service);
			if (service == null)
			{
				return NotFound($"Serviço com ID {id_service} não encontrado.");
			}

			await _serviceService.DeleteServiceAsync(id_service);
			return NoContent(); // Retorna 204 quando a exclusão for bem-sucedida
		}
	}
}
