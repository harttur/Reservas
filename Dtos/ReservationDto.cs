using MongoDB.Bson.Serialization.Attributes;

namespace Reservas.Dtos
{
	public class ReservationDto
	{
		/// <summary>
		/// Identificador único da reserva.
		/// </summary>
		public string IdReservation { get; set; }

		/// <summary>
		/// Identificador do usuário que fez a reserva.
		/// </summary>
		public string IdUser { get; set; }

		/// <summary>
		/// Identificador do serviço reservado.
		/// </summary>
		public string IdService { get; set; }

		/// <summary>
		/// Nome do cliente que fez a reserva.
		/// </summary>
		public string NomeCliente { get; set; }

		/// <summary>
		/// Data e hora da reserva.
		/// </summary>
		public DateTime ReservationDateTime { get; set; }
	}
}
