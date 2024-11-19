using Reservas.Dtos;
using Reservas.Models;
using System.Collections.Generic;
using System.Linq;

namespace Reservas.Mappers
{
	public class ServiceMapper
	{
		/// <summary>
		/// Converte uma entidade Service para um DTO ServiceDto.
		/// </summary>
		public static ServiceDto ToDto(Service service)
		{
			if (service == null) return null;

			return new ServiceDto
			{
				Id_Service = service.Id_Service,
				descricao = service.descricao
			};
		}

		/// <summary>
		/// Converte um DTO ServiceDto para uma entidade Service.
		/// </summary>
		public static Service ToEntity(ServiceDto serviceDto)
		{
			if (serviceDto == null) return null;

			return new Service
			{
				Id_Service = serviceDto.Id_Service,
				descricao = serviceDto.descricao
			};
		}

		/// <summary>
		/// Converte uma lista de entidades Service para uma lista de DTOs ServiceDto.
		/// </summary>
		public static List<ServiceDto> ToDtoList(List<Service> services)
		{
			// Verifica se a lista é nula ou vazia antes de tentar realizar a conversão
			return services?.Select(ToDto).ToList();
		}

		/// <summary>
		/// Converte uma lista de DTOs ServiceDto para uma lista de entidades Service.
		/// </summary>
		public static List<Service> ToEntityList(List<ServiceDto> serviceDtos)
		{
			// Verifica se a lista é nula ou vazia antes de tentar realizar a conversão
			return serviceDtos?.Select(ToEntity).ToList();
		}
	}
}
