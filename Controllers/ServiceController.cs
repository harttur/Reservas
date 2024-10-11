using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Reservas.Dtos;
using Reservas.Models;
using Reservas.Services;
using Reservas.Services.Contract;

namespace Reservas.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService servicesService)
        {
            _serviceService = servicesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceDto>>> GetAllServices()
        {
            var service = await _serviceService.GetAllServicesAsync();
            return service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateService([FromBody] ServiceDto serviceDto)
        {

            Service service = await _serviceService.CreateServiceAsync(serviceDto);

            return CreatedAtAction(nameof(GetServiceById),
                                   new { id_service = service.Id_Service},
                                   serviceDto);
        }


        [HttpGet("{Id_Service}")]
        public async Task<ActionResult<ServiceDto>> GetServiceById(string id_service)
        {
            var service = await _serviceService.GetServiceByIdAsync(id_service);

            if (service == null)
            {
                return NotFound(); 
            }

            return service; 
        }

        /*[HttpPut("{id_servie}")]
        public async Task<bool> UpdateServiceAsync(string id_service, ServiceDto serviceDto)
        {
            // Lógica de atualização
            return true; // ou false se não for bem-sucedido
        }*/

        [HttpPut("{id_service}")]
        public async Task<IActionResult> UpdateServiceAsync(string id_service, ServiceDto serviceDto)
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

            return Ok(updatedService);
        }



        [HttpDelete("{id_servie}")]
        public async Task<ActionResult> DeleteService(string Id_service)
        {
            await _serviceService.DeleteServiceAsync(Id_service);
            return NoContent();
        }

    }
}
