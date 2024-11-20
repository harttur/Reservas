using Microsoft.IdentityModel.Tokens;
using Reservas.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Reservas.Services
{
	/// <summary>
	/// Serviço responsável por gerar tokens JWT.
	/// </summary>
	public class JwtTokenService
	{
		private readonly JwtConfiguration _jwtConfig;

		/// <summary>
		/// Construtor que recebe as configurações do JWT.
		/// </summary>
		/// <param name="jwtConfig">Instância de configurações do JWT.</param>
		public JwtTokenService(JwtConfiguration jwtConfig)
		{
			_jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig), "Configuração do JWT não pode ser nula.");

			// Verificando se as configurações essenciais estão presentes
			if (string.IsNullOrEmpty(_jwtConfig.Secret))
				throw new ArgumentException("A chave secreta do JWT não pode ser nula ou vazia.");
			if (string.IsNullOrEmpty(_jwtConfig.Issuer))
				throw new ArgumentException("O emissor do JWT não pode ser nulo ou vazio.");
			if (string.IsNullOrEmpty(_jwtConfig.Audience))
				throw new ArgumentException("O público-alvo do JWT não pode ser nulo ou vazio.");
		}

		/// <summary>
		/// Gera um token JWT com as informações fornecidas.
		/// </summary>
		/// <param name="userId">Identificador único do usuário.</param>
		/// <param name="userName">Nome do usuário.</param>
		/// <returns>Token JWT como string.</returns>
		public string GenerateToken(string userId, string userName)
		{
			try
			{
				// Define as informações do token (claims)
				var claims = new[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, userId),
					new Claim(JwtRegisteredClaimNames.UniqueName, userName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // ID único do token
                };

				// Chave de segurança
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
				// Loga o erro ou manipula a exceção de forma apropriada
				throw new InvalidOperationException("Erro ao gerar o token JWT.", ex);
			}
		}
	}
}
