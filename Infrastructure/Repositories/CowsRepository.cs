using Domain.Models.Cows;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class CowsRepository : BaseRepository<Cow>, ICowsRepository
    {
        private const string FARM_INDEX = "CowFarmIndex";
        private const string STATE_INDEX = "CowStateIndex";
        public CowsRepository(MongoContext mongoContext) : base(mongoContext)
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
