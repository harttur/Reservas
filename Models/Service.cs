using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Reservas.Models
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id_Service { get; set; }

        [BsonElement("descricao")]
        public string? descricao { get; set; }
    }
}
