using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Reservas.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id_reservation { get; set; }

        [BsonElement("Id_reservation")]
        public string? Id_user { get; set; }

        [BsonElement("Id_Services")]
        public string? Id_services { get; set; }

        [BsonElement("Nome_cliente")]
        public string? Nome_cliente { get; set; }

        [BsonElement("Data_time")]
        public string? Data_time { get; set; }
    }
}
