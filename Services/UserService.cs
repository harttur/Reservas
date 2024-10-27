using Microsoft.IdentityModel.Tokens;
using Reservas.Dtos;
using Reservas.Mappers;
using Reservas.Models;
using Reservas.Repository.Contract;
using Reservas.Services.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;

        public UserService(IUserRepository userRepository, string jwtSecret)
        {
            _userRepository = userRepository;
            _jwtSecret = jwtSecret;
        }

        public async Task<Models.User> CreateUserAsync(UserDto userDto)
        {
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

            var updatedUser = UserMapper.ToEntity(userDto);
            await _userRepository.UpdateAsync(id_user, updatedUser);
        }

        String IUserService.Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null || !VerifyPassword(storedPassword: user.password, providedPassword: password))
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");
            }
            return GenerateJwtToken(user);
        }

        public string GenerateJwtToken(User user)
        {
            // Define as reivindicações (claims) do token
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null, // Defina o emissor, se necessário
                audience: null, // Defina o público, se necessário
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Defina o tempo de expiração conforme necessário
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static bool VerifyPassword(string storedPassword, string providedPassword)
        {
            // Lógica para verificar se a senha fornecida corresponde à senha armazenada
            return storedPassword == providedPassword; // Exemplo simplificado
        }
        string IUserService.GenerateJwtToken(User user)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwtToken(string user)
        {
            // Define as reivindicações (claims) do token
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null, // Defina o emissor, se necessário
                audience: null, // Defina o público, se necessário
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Defina o tempo de expiração conforme necessário
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
