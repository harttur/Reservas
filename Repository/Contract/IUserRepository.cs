using Reservas.Models;

namespace Reservas.Repository.Contract
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id_user);
        Task CreateAsync(User entity);
        Task UpdateAsync(string id_user, User entity);
        Task DeleteAsync(string id_user);
    }
}
