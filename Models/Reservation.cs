using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Reservas.Models
{
	/// <summary>
	/// Representa uma reserva no sistema, contendo informações sobre o usuário, serviço, 
	/// nome do cliente e data/hora da reserva.
	/// </summary>
	public class Reservation
	{
		/// <summary>
		/// Identificador único da reserva.
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id_reservation { get; set; }

		/// <summary>
		/// Identificador do usuário que fez a reserva.
		/// </summary>
		[BsonElement("Id_user")]
		public string? Id_user { get; set; }

		/// <summary>
		/// Identificador do serviço reservado.
		/// </summary>
		[BsonElement("Id_Services")]
		public string? Id_services { get; set; }

		/// <summary>
		/// Nome do cliente que fez a reserva.
		/// </summary>
		[BsonElement("Nome_cliente")]
		public string? Nome_cliente { get; set; }

		/// <summary>
		/// Data e hora da reserva.
		/// </summary>
		[BsonElement("Data_time")]
		public DateTime? Data_time { get; set; }
	}
}
