using MongoDB.Driver;
using Reservas.Models;

namespace Reservas.Data
{
	/// <summary>
	/// Contexto do MongoDB para fornecer acesso às coleções do banco de dados.
	/// </summary>
	public class MongoDbContext
	{
		private readonly IMongoDatabase _database;

		/// <summary>
		/// Construtor para inicializar o contexto com o cliente MongoDB e nome do banco de dados.
		/// </summary>
		/// <param name="mongoClient">Cliente MongoDB.</param>
		/// <param name="databaseName">Nome do banco de dados.</param>
		public MongoDbContext(IMongoClient mongoClient, string databaseName)
		{
			_database = mongoClient.GetDatabase(databaseName);
		}

		/// <summary>
		/// Coleção de usuários no MongoDB.
		/// </summary>
		public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

		/// <summary>
		/// Coleção de serviços no MongoDB.
		/// </summary>
		public IMongoCollection<Service> Services => _database.GetCollection<Service>("Services");

		/// <summary>
		/// Coleção de reservas no MongoDB.
		/// </summary>
		public IMongoCollection<Reservation> Reservations => _database.GetCollection<Reservation>("Reservations");
	}
}
