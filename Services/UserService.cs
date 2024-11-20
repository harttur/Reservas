using Microsoft.IdentityModel.Tokens;
using Reservas.Dtos;
using Reservas.Mappers;
using Reservas.Models;
using Reservas.Repository.Contract;
using Reservas.Services.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Reservas.Services
{
    public class UserService : IUserService
    {
		private readonly IUserRepository _userRepository;
		private readonly string _jwtSecret;
		private readonly string _jwtIssuer;
		private readonly string _jwtAudience;

		public UserService(IUserRepository userRepository, string jwtSecret, string jwtIssuer, string jwtAudience)
		{
			_userRepository = userRepository;
			_jwtSecret = jwtSecret;
			_jwtIssuer = jwtIssuer;
			_jwtAudience = jwtAudience;
		}

		public async Task<Models.User> CreateUserAsync(UserDto userDto)
		{
			// Hash da senha antes de salvar
			userDto.Password = HashPassword(userDto.Password);

			var user = UserMapper.ToEntity(userDto);
			await _userRepository.CreateAsync(user);

			return user;
		}

		public async Task DeleteUserAsync(string id_user)
		{
			var user = await _userRepository.GetByIdAsync(id_user);
			if (user == null)
			{
				return;
			}
			await _userRepository.DeleteAsync(id_user);
		}

		public async Task<List<UserDto>> GetAllUsersAsync()
		{
			var user = await _userRepository.GetAllAsync();
			return UserMapper.ToDtoList(user);
		}

		public async Task<UserDto> GetUserByIdAsync(string id_user)
		{
			var user = await _userRepository.GetByIdAsync(id_user);
			if (user == null)
			{
				return null;
			}

			return UserMapper.ToDto(user);
		}

		public async Task UpdateUserAsync(string id_user, UserDto userDto)
		{
			var user = await _userRepository.GetByIdAsync(id_user);
			if (user == null)
			{
				return;
			}

			// Hash da senha antes de atualizar
			userDto.Password = HashPassword(userDto.Password);

			var updatedUser = UserMapper.ToEntity(userDto);
			await _userRepository.UpdateAsync(id_user, updatedUser);
		}

        public string GenerateJwtToken(string name)
        {
			var claims = new[]
			{
                 new Claim(ClaimTypes.Name, name),
			     new Claim(ClaimTypes.Role, "User")
            };

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtIssuer,
				audience: _jwtAudience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(180),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private bool VerifyPassword(string storedPassword, string providedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(providedPassword, storedPassword); // Verifica com bcrypt
		}

		private string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password); // Gera o hash com bcrypt
		}

        public string Authenticate(string username, string password)
        {
            // Simulando uma busca de usuário no banco de dados
            var users = new List<User>
    {
        new User { Name = "admin", Password = BCrypt.Net.BCrypt.HashPassword("1234") },
        new User { Name = "user1", Password = BCrypt.Net.BCrypt.HashPassword("password1") }
    };

            // Procurar o usuário pelo nome de usuário
            var user = users.FirstOrDefault(u => u.Name == username);

            if (user == null)
            {
                return "Usuário não encontrado";
            }

            // Verificar a senha usando BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isPasswordValid)
            {
                return "Senha inválida";
            }

            // Gerar um token ou retornar uma mensagem de sucesso
            return "Autenticação bem-sucedida! Token de acesso: [ExemploToken]";
        }

    }
}
