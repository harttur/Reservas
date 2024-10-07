using MongoDB.Driver;
using Reservas.Models;

namespace Reservas.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("User");
        public IMongoCollection<Service> Services => _database.GetCollection<Service>("Service");
        public IMongoCollection<Reservation> Reservations => _database.GetCollection<Reservation>("Reservation");
    }
}
