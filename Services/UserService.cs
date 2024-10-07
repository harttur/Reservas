using Reservas.Dtos;
using Reservas.Mappers;
using Reservas.Repository.Contract;
using Reservas.Services.Contract;
using System.Threading.Tasks;

namespace Reservas.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}
