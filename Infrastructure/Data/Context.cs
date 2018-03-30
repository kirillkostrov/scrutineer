using Core.Entities;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class Context
    {
        private readonly IMongoDatabase _database;

        public Context(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Standart> Standarts =>
            _database.GetCollection<Standart>("Standart");
    }
}