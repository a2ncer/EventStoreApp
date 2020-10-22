using Domain.Models;
using Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TModel> : IRepository<TModel> where TModel : DomainModel
    {
        protected IMongoCollection<TModel> _latestSnapshot;
        private readonly List<string> _indexCache;
        protected BaseRepository(MongoContext mongoContext)
        {
            _latestSnapshot = mongoContext.GetCollection<TModel>($"{typeof(TModel).Name}Snapshot");
            _indexCache = new List<string>();
            using (var cursor = _latestSnapshot.Indexes.List())
            {
                _indexCache.AddRange(cursor.ToEnumerable().Select(x => x.ToString()));
            }
        }

        public Task<TModel> DeleteAsync(Guid id)
        {
            return _latestSnapshot.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public Task<List<TModel>> GetAllAsync()
        {
            return _latestSnapshot.Find(x => true).ToListAsync();
        }

        public Task<TModel> GetAsync(Guid id)
        {
            return _latestSnapshot.Find(model => model.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveAsync(TModel model)
        {
            var updated = await _latestSnapshot.FindOneAndReplaceAsync(x => x.Id == model.Id, model);
            if (updated == default)
            {
                await _latestSnapshot.InsertOneAsync(model);
            }
        }

        protected void CreateIndex(string name, Expression<Func<TModel, object>> field)
        {
            if (_indexCache.Contains(name))
            {
                return;
            }
            var indexOptions = new CreateIndexOptions
            {
                Name = name
            };
            var indexKeys = Builders<TModel>.IndexKeys.Ascending(field);
            var indexModel = new CreateIndexModel<TModel>(indexKeys, indexOptions);

            _latestSnapshot.Indexes.CreateOneAsync(indexModel).GetAwaiter().GetResult();
            _indexCache.Add(name);
        }
    }
}
