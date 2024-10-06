using MongoDB.Driver;
using Reservas.Models;

namespace Reservas.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("User");
        public IMongoCollection<Service> Services => _database.GetCollection<Service>("Service");
        public IMongoCollection<Reservetion> Reservetions => _database.GetCollection<Reservetion>("Reservetion");
    }
}
