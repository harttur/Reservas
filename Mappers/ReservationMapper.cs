using Reservas.Dtos;
using Reservas.Models;
using System.Collections.Generic;
using System.Linq;

namespace Reservas.Mappers
{
	public class ReservationMapper
	{
		/// <summary>
		/// Converte uma entidade Reservation para um DTO ReservationDto.
		/// </summary>
		public static ReservationDto ToDto(Reservation reservation)
		{
			if (reservation == null) return null;

			return new ReservationDto
			{
				Id_user = reservation.Id_user,
				Id_services = reservation.Id_services,
				Nome_cliente = reservation.Nome_cliente,
				Data_time = reservation.Data_time
			};
		}

		/// <summary>
		/// Converte um DTO ReservationDto para uma entidade Reservation.
		/// </summary>
		public static Reservation ToEntity(ReservationDto reservationDto)
		{
			if (reservationDto == null) return null;

			return new Reservation
			{
				Id_user = reservationDto.Id_user,
				Id_services = reservationDto.Id_services,
				Nome_cliente = reservationDto.Nome_cliente,
				Data_time = reservationDto.Data_time
			};
		}

		/// <summary>
		/// Converte uma lista de entidades Reservation para uma lista de DTOs ReservationDto.
		/// </summary>
		public static List<ReservationDto> ToDtoList(List<Reservation> reservations)
		{
			// Verifica se a lista é nula ou vazia antes de tentar realizar a conversão
			return reservations?.Select(ToDto).ToList();
		}

		/// <summary>
		/// Converte uma lista de DTOs ReservationDto para uma lista de entidades Reservation.
		/// </summary>
		public static List<Reservation> ToEntityList(List<ReservationDto> reservationDtos)
		{
			// Verifica se a lista é nula ou vazia antes de tentar realizar a conversão
			return reservationDtos?.Select(ToEntity).ToList();
		}
	}
}
