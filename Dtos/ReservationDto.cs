using MongoDB.Bson.Serialization.Attributes;

namespace Reservas.Dtos
{
    public class ReservationDto
    {
        public string? Id_reservation { get; set; }
        public string? Id_user { get; set; }
        public string? Id_services { get; set; }
        public string? Nome_cliente { get; set; }
        public string? Data_time { get; set; }
    }
}
