using Reservas.Dtos;
using Reservas.Models;

namespace Reservas.Mappers
{
    public class ReservationMapper
    {
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

        public static List<ReservationDto> ToDtoList(List<Reservation> reservation)

        {
            return reservation?.Select(ToDto).ToList();
        }

        public static List<Reservation> ToEntity(List<ReservationDto> reservationDto)

        {
            return reservationDto?.Select(ToEntity).ToList();
        }
    }
}
