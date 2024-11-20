using Reservas.Dtos;
using Reservas.Models;

namespace Reservas.Services.Contract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id_user);
        Task<Models.User> CreateUserAsync(UserDto user);
        Task UpdateUserAsync(string id_user, UserDto userdto);
        Task DeleteUserAsync(string id_user);
        String Authenticate(string name, string password); /* !! */
        string GenerateJwtToken(string name);
        object ValidateUser(object name, object password);
    }
}


