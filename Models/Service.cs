using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Reservas.Models
{
	/// <summary>
	/// Representa um serviço disponível no sistema, contendo uma descrição.
	/// </summary>
	public class Service
	{
		/// <summary>
		/// Identificador único do serviço.
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id_Service { get; set; }

		/// <summary>
		/// Descrição do serviço.
		/// </summary>
		[BsonElement("descricao")]
		public string? Descricao { get; set; }
	}
}
