using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepository<TModel>
    {
        Task<TModel> GetAsync(Guid id);
        Task<List<TModel>> GetAllAsync();
        Task SaveAsync(TModel model);
        Task<TModel> DeleteAsync(Guid id);
    }
}
