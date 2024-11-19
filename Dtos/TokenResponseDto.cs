namespace Reservas.Dtos
{
	/// <summary>
	/// Representa a resposta de um token de autenticação, contendo o token JWT gerado e sua data de expiração.
	/// </summary>
	public class TokenResponseDto
	{
		/// <summary>
		/// Token JWT gerado.
		/// </summary>
		public string Token { get; }

		/// <summary>
		/// Data e hora em que o token expira.
		/// </summary>
		public DateTime Expiration { get; }

		/// <summary>
		/// Construtor para inicializar o Token e a Expiration.
		/// </summary>
		/// <param name="token">Token JWT gerado.</param>
		/// <param name="expiration">Data e hora da expiração do token.</param>
		public TokenResponseDto(string token, DateTime expiration)
		{
			Token = token;
			Expiration = expiration;
		}
	}
}
