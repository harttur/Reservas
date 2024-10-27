using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Reservas.Data;
using Reservas.Models;
using Reservas.Repository.Contract;
using System.Collections.Generic;
using static MongoDB.Driver.WriteConcern;

namespace Reservas.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbContext context)
        {
            _users = context.Users;
           
        }
        public User GetUserByUsername(string username)
        {
            // Busca o usuário pelo nome de usuário, ignorando maiúsculas e minúsculas
            return _users.AsQueryable()
                .FirstOrDefault(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id_user)
        {
            return await _users.Find<User>(user => user.Id_user == id_user).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)

        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateAsync(string id_user, User user)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id_user, id_user);
            var update = Builders<User>.Update
                .Set(newUser => newUser.Name, user.Name)
                .Set(newUser => newUser.password, user.password);

            await _users.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id_user)
        {
            await _users.DeleteOneAsync(user => id_user == user.Id_user);
        }

        User IUserRepository.GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
