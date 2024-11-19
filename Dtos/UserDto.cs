using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Reservas.Dtos
{
	/// <summary>
	/// DTO para representar os dados de um usuário.
	/// </summary>
	public class UserDto
	{
		/// <summary>
		/// Nome do usuário.
		/// </summary>
		[BsonElement("name")]
		public string Name { get; set; }

		/// <summary>
		/// Senha do usuário.
		/// </summary>
		[BsonElement("password")]
		public string Password { get; set; }

		/// <summary>
		/// Construtor para inicializar os dados do usuário.
		/// </summary>
		/// <param name="name">Nome do usuário.</param>
		/// <param name="password">Senha do usuário.</param>
		public UserDto(string name, string password)
		{
			Name = name;
			Password = password;
		}
	}
}
