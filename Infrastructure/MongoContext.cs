using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MongoContext
    {
        private readonly IMongoDatabase _db;
        private readonly MongoSettings _configuration;

        public MongoContext(IOptions<MongoSettings> configuration) : this(configuration.Value)
        {
        }

        public MongoContext(MongoSettings configuration)
        {
            var client = new MongoClient(configuration.Connection);
            _db = client.GetDatabase(configuration.DatabaseName);
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
            _configuration = configuration;
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

        public Task DropDatabaseAsync()
        {
            return _db.Client.DropDatabaseAsync(_configuration.DatabaseName);
        }
    }
}
