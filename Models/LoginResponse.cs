namespace Reservas.Models
{
	/// <summary>
	/// Classe que representa a resposta ao processo de login, incluindo o token JWT,
	/// a data de expiração e o nome do usuário autenticado.
	/// </summary>
	public class LoginResponse
	{
		/// <summary>
		/// Token JWT gerado após o login bem-sucedido.
		/// </summary>
		public string Token { get; }

		/// <summary>
		/// Data e hora de expiração do token.
		/// </summary>
		public DateTime Expiration { get; }

		/// <summary>
		/// Nome do usuário que fez login.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Construtor para inicializar a resposta de login com os dados necessários.
		/// </summary>
		/// <param name="token">Token JWT gerado.</param>
		/// <param name="expiration">Data e hora de expiração do token.</param>
		/// <param name="name">Nome do usuário.</param>
		public LoginResponse(string token, DateTime expiration, string name)
		{
			Token = token;
			Expiration = expiration;
			Name = name;
		}
	}
}
