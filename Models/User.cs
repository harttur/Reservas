using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reservas.Models
{
	/// <summary>
	/// Representa um usuário no sistema.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Identificador único do usuário.
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id_user { get; set; }

		/// <summary>
		/// Nome do usuário.
		/// </summary>
		[BsonElement("name")]
		public string? Name { get; set; }

		/// <summary>
		/// Senha do usuário.
		/// </summary>
		[BsonElement("password")]
		public string? Password { get; set; }
	}
}
