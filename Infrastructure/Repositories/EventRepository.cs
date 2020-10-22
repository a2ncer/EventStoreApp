using Domain.Models;
using Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EventRepository<TModel> : IEventRepository<TModel> where TModel : DomainModel
    {
        private const string DATE_INDEX_NAME = "DateIndex";
        private const string EVENT_TYPE_INDEX_NAME = "EventTypeIndex";
        private readonly IMongoCollection<DomainEvent<TModel>> _eventStore;
        private readonly List<string> _indexCache;

        public EventRepository(MongoContext mongoContext)
        {
            _eventStore = mongoContext.GetCollection<DomainEvent<TModel>>($"{typeof(TModel).Name}Events");
            _indexCache = new List<string>();
            EnsureIndexesCreated();
        }

        public Task CreateAsync(TModel model)
        {
            return _eventStore.InsertOneAsync(new DomainEvent<TModel>() { EventType = EventType.Created, Model = model });
        }

        public Task DeleteAsync(TModel model)
        {
            return _eventStore.InsertOneAsync(new DomainEvent<TModel>() { EventType = EventType.Deleted, Model = model });
        }

        public async Task<IEnumerable<TModel>> ProjectAsync(DateTimeOffset occuredDate)
        {
            var history = await _eventStore.Find(x => x.OccuredAt <= occuredDate).ToListAsync();
            var projection = new Dictionary<Guid, TModel>();
            foreach (var @event in history)
            {
                switch (@event.EventType)
                {
                    case EventType.Created: projection.Add(@event.Model.Id, @event.Model); break;
                    case EventType.Updated: projection[@event.Model.Id] = @event.Model; break;
                    case EventType.Deleted: projection.Remove(@event.Model.Id); break;
                }
            }
            return projection.Values;
        }

        public Task UpdateAsync(TModel model)
        {
            return _eventStore.InsertOneAsync(new DomainEvent<TModel>() { EventType = EventType.Updated, Model = model });
        }

        protected void CreateIndex(string name, Expression<Func<DomainEvent<TModel>, object>> field)
        {
            if (_indexCache.Contains(name))
            {
                return;
            }
            var indexOptions = new CreateIndexOptions
            {
                Name = name
            };
            var indexKeys = Builders<DomainEvent<TModel>>.IndexKeys.Ascending(field);
            var indexModel = new CreateIndexModel<DomainEvent<TModel>>(indexKeys, indexOptions);

            _eventStore.Indexes.CreateOneAsync(indexModel).GetAwaiter().GetResult();
            _indexCache.Add(name);
        }

        private void EnsureIndexesCreated()
        {
            CreateIndex(DATE_INDEX_NAME, x => x.OccuredAt);
            CreateIndex(EVENT_TYPE_INDEX_NAME, x => x.EventType);
        }
    }
}
