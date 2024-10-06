using Reservas.Dtos;
using Reservas.Models;

namespace Reservas.Mappers
{
    public class ServiceMapper
    {
        public static ServiceDto ToDto(Service service)
        {
            if (service == null) return null;

            return new ServiceDto
            {
                Id_Service = service.Id_Service,
                descricao = service.descricao
            };

        }

        public static Service ToEntity(ServiceDto serviceDto)
        {
            if (serviceDto == null) return null;

            return new Service
            {
                Id_Service = serviceDto.Id_Service,
                descricao = serviceDto.descricao
            };

        }

        public static List<ServiceDto> ToDtoList(List<Service> service)

        {
            return service?.Select(ToDto).ToList();
        }

        public static List<Service> ToEntity(List<ServiceDto> serviceDto)

        {
            return serviceDto?.Select(ToEntity).ToList();
        }
    }
}
