using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Infrastructure
{
    public class MongoContext
    {
        private readonly IMongoDatabase _db;

        public MongoContext(IOptions<MongoSettings> configuration)
        {
            var client = new MongoClient(configuration.Value.Connection);
            _db = client.GetDatabase(configuration.Value.DatabaseName);
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
