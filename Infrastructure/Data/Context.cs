using Microsoft.Extensions.Options;
using Models;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class Context
    {
        private readonly IMongoDatabase _database = null;

        public Context(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }
    }
}
