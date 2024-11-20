using Microsoft.IdentityModel.Tokens;
using Reservas.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Reservas.Services
{
	/// <summary>
	/// Servi�o respons�vel por gerar tokens JWT.
	/// </summary>
	public class JwtTokenService
	{
		private readonly JwtConfiguration _jwtConfig;

		/// <summary>
		/// Construtor que recebe as configura��es do JWT.
		/// </summary>
		/// <param name="jwtConfig">Inst�ncia de configura��es do JWT.</param>
		public JwtTokenService(JwtConfiguration jwtConfig)
		{
			_jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig), "Configura��o do JWT n�o pode ser nula.");

			// Verificando se as configura��es essenciais est�o presentes
			if (string.IsNullOrEmpty(_jwtConfig.Secret))
				throw new ArgumentException("A chave secreta do JWT n�o pode ser nula ou vazia.");
			if (string.IsNullOrEmpty(_jwtConfig.Issuer))
				throw new ArgumentException("O emissor do JWT n�o pode ser nulo ou vazio.");
			if (string.IsNullOrEmpty(_jwtConfig.Audience))
				throw new ArgumentException("O p�blico-alvo do JWT n�o pode ser nulo ou vazio.");
		}

		/// <summary>
		/// Gera um token JWT com as informa��es fornecidas.
		/// </summary>
		/// <param name="userId">Identificador �nico do usu�rio.</param>
		/// <param name="userName">Nome do usu�rio.</param>
		/// <returns>Token JWT como string.</returns>
		public string GenerateToken(string userId, string userName)
		{
			try
			{
				// Define as informa��es do token (claims)
				var claims = new[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, userId),
					new Claim(JwtRegisteredClaimNames.UniqueName, userName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // ID �nico do token
                };

				// Chave de seguran�a
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
				var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				// Cria o token
				var token = new JwtSecurityToken(
					issuer: _jwtConfig.Issuer,
					audience: _jwtConfig.Audience,
					claims: claims,
					expires: DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationMinutes),
					signingCredentials: credentials
				);

				// Retorna o token serializado
				return new JwtSecurityTokenHandler().WriteToken(token);
			}
			catch (Exception ex)
			{
				// Loga o erro ou manipula a exce��o de forma apropriada
				throw new InvalidOperationException("Erro ao gerar o token JWT.", ex);
			}
		}
	}
}
