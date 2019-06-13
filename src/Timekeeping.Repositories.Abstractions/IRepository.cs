using System.Collections.Generic;
using System.Threading.Tasks;

namespace Timekeeping.Repositories.Abstractions
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<TModel> GetAsync(int id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> SaveAsync(TModel model);
        Task DeleteAsync(int id);
    }
}
