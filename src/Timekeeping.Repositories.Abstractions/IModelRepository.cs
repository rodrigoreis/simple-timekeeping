using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Timekeeping.Repositories.Abstractions
{
    public interface IModelRepository<TModel> : IRepository
        where TModel : class, IModel
    {
        Task<IQueryable<TModel>> GetQueryableAsync(params string[] expands);
        Task<IEnumerable<TModel>> GetAllAsync(params string[] expands);
        Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate, params string[] expands);
        Task AddAsync(TModel model, Expression<Func<TModel, bool>> predicate);
        Task UpdateAsync(TModel model, Expression<Func<TModel, bool>> predicate);
        Task DeleteAsync(TModel model, Expression<Func<TModel, bool>> predicate);
    }
}
