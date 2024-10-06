using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reservas.Models
{
        public class User
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string? Id_user { get; set; }

            [BsonElement("name")]
            public string? Name { get; set; }

            [BsonElement("password")]   
            public string? password { get; set; }
        }
    
}
