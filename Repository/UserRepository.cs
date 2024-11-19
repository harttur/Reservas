using MongoDB.Bson;
using MongoDB.Driver;
using Reservas.Data;
using Reservas.Models;
using Reservas.Repository.Contract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservas.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly IMongoCollection<User> _users;

		public UserRepository(MongoDbContext context)
		{
			_users = context.Users;
		}

		/// <summary>
		/// Busca um usuário pelo nome de usuário, ignorando maiúsculas e minúsculas.
		/// </summary>
		public User GetUserByUsername(string username)
		{
			// Busca o usuário pelo nome de usuário
			var filter = Builders<User>.Filter.Eq(u => u.Name, username);
			return _users.Find(filter).FirstOrDefault();
		}

		/// <summary>
		/// Recupera todos os usuários.
		/// </summary>
		public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			// Retorna todos os usuários
			return await _users.Find(Builders<User>.Filter.Empty)
				.ToListAsync(cancellationToken);
		}

		/// <summary>
		/// Recupera um usuário pelo ID.
		/// </summary>
		public async Task<User> GetByIdAsync(string id_user, CancellationToken cancellationToken = default)
		{
			var filter = Builders<User>.Filter.Eq(u => u.Id_user, id_user);
			return await _users.Find(filter).FirstOrDefaultAsync(cancellationToken);
		}

		/// <summary>
		/// Cria um novo usuário.
		/// </summary>
		public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
		{
			await _users.InsertOneAsync(user, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Atualiza um usuário existente.
		/// </summary>
		public async Task UpdateAsync(string id_user, User user, CancellationToken cancellationToken = default)
		{
			var filter = Builders<User>.Filter.Eq(u => u.Id_user, id_user);
			var update = Builders<User>.Update
				.Set(u => u.Name, user.Name)
				.Set(u => u.password, user.password);

			await _users.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Deleta um usuário pelo ID.
		/// </summary>
		public async Task DeleteAsync(string id_user, CancellationToken cancellationToken = default)
		{
			var filter = Builders<User>.Filter.Eq(u => u.Id_user, id_user);
			await _users.DeleteOneAsync(filter, cancellationToken);
		}
	}
}
