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
        User Authenticate(string username, string password); /* !! */
        string GenerateJwtToken(User user);  /* !! */
    }
}


