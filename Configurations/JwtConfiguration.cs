namespace Reservas.Configurations
{
	/// <summary>
	/// Representa as configurações necessárias para geração e validação de tokens JWT.
	/// </summary>
	public class JwtConfiguration
	{
		/// <summary>
		/// Chave secreta usada para assinatura do token.
		/// </summary>
		public string Secret { get; set; }

		/// <summary>
		/// Emissor do token (Issuer).
		/// </summary>
		public string Issuer { get; set; }

		/// <summary>
		/// Público alvo do token (Audience).
		/// </summary>
		public string Audience { get; set; }

		/// <summary>
		/// Tempo de expiração do token em minutos.
		/// </summary>
		public int ExpirationMinutes { get; set; }

		/// <summary>
		/// Construtor que inicializa as configurações do JWT.
		/// </summary>
		/// <param name="secret">Chave secreta para assinatura.</param>
		/// <param name="issuer">Emissor do token.</param>
		/// <param name="audience">Público alvo do token.</param>
		/// <param name="expirationMinutes">Tempo de expiração em minutos.</param>
		/// <exception cref="ArgumentException">Lançado se os parâmetros obrigatórios estiverem inválidos.</exception>
		public JwtConfiguration(string secret, string issuer, string audience, int expirationMinutes)
		{
			if (string.IsNullOrWhiteSpace(secret))
				throw new ArgumentException("A chave secreta não pode ser nula ou vazia.", nameof(secret));

			if (string.IsNullOrWhiteSpace(issuer))
				throw new ArgumentException("O emissor não pode ser nulo ou vazio.", nameof(issuer));

			if (string.IsNullOrWhiteSpace(audience))
				throw new ArgumentException("O público alvo não pode ser nulo ou vazio.", nameof(audience));

			if (expirationMinutes <= 0)
				throw new ArgumentException("O tempo de expiração deve ser maior que zero.", nameof(expirationMinutes));

			Secret = secret;
			Issuer = issuer;
			Audience = audience;
			ExpirationMinutes = expirationMinutes;
		}
	}
}
