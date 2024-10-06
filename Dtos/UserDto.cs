using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Reservas.Dtos
{
    public class UserDto
    {
        public string? Id_user { get; set; }
        public string? Name { get; set; }
        public string? password { get; set; }  
    }
}
