using Domain.Models.Sensors;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class SensorsRepository : BaseRepository<Sensor>, ISensorsRepository
    {
        private const string FARM_INDEX = "SensorFarmIndex";
        private const string STATE_INDEX = "SensorStateIndex";
        public SensorsRepository(MongoContext mongoContext) : base(mongoContext)
        {
            EnsureIndexesCreated();
        }
        private void EnsureIndexesCreated()
        {
            CreateIndex(FARM_INDEX, x => x.FarmId);
            CreateIndex(STATE_INDEX, x => x.State);
        }
    }
}
