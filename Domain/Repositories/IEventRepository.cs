using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IEventRepository<TModel>
    {
        Task<IEnumerable<TModel>> ProjectAsync(DateTimeOffset occuredDate);
        Task CreateAsync(TModel model, DateTimeOffset? occuredAt = default);
        Task UpdateAsync(TModel model, DateTimeOffset? occuredAt = default);
        Task DeleteAsync(TModel model, DateTimeOffset? occuredAt = default);
    }
}
