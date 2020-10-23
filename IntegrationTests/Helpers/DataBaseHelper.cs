using Domain.Models.Cows;
using Domain.Models.Sensors;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using System.Threading.Tasks;

namespace IntegrationTests.Helpers
{
    public class DataBaseHelper
    {
        private readonly MongoContext _mongoContext;
        public IEventRepository<Cow> CowEventStore => new EventRepository<Cow>(_mongoContext);
        public IEventRepository<Sensor> SensorEventStore => new EventRepository<Sensor>(_mongoContext);
        public DataBaseHelper(string connection, string database)
        {
            var settings = new MongoSettings
            {
                Connection = connection,
                DatabaseName = database
            };
            _mongoContext = new MongoContext(settings);
        }

        public Task ClearDatabaseAsync()
        {
            return _mongoContext.DropDatabaseAsync();
        }
    }
}
