using MongoDB.Bson.Serialization.Attributes;

namespace Reservas.Dtos
{
    public class ServiceDto
    {
        public string? Id_Service { get; set; }
        public string? descricao { get; set; }
    }
}

