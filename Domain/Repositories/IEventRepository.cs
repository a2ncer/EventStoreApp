using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IEventRepository<TModel>
    {
        Task<IEnumerable<TModel>> ProjectAsync(DateTimeOffset occuredDate);
        Task CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(TModel model);
    }
}
