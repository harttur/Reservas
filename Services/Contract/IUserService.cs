

using Reservas.Dtos;

namespace Reservas.Services.Contract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id_user);
        Task CreateUserAsync(UserDto user);
        Task UpdateUserAsync(string id_user, UserDto userdto);
        Task DeleteUserAsync(string id_user);   
    }
}
