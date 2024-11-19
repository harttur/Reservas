using MongoDB.Bson.Serialization.Attributes;

namespace Reservas.Dtos
{
	public class ServiceDto
	{
		/// <summary>
		/// Identificador único do serviço.
		/// </summary>
		public string IdService { get; set; }

		/// <summary>
		/// Descrição do serviço.
		/// </summary>
		public string Descricao { get; set; }
	}
}
