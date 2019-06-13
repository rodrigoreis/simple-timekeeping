using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Timekeeping.Repositories.Abstractions
{
    public interface IRepository
    {
        Task<IQueryable<TModel>> GetQueryableAsync<TModel>(params string[] expands)
            where TModel : class, IModel;
        Task<IEnumerable<TModel>> GetAllAsync<TModel>(params string[] expands)
            where TModel : class, IModel;
        Task<TModel> GetAsync<TModel>(Expression<Func<TModel, bool>> predicate, params string[] expands)
            where TModel : class, IModel;
        Task AddAsync<TModel>(TModel model, Expression<Func<TModel, bool>> predicate)
            where TModel : class, IModel;
        Task UpdateAsync<TModel>(TModel model, Expression<Func<TModel, bool>> predicate)
            where TModel : class, IModel;
        Task DeleteAsync<TModel>(TModel model, Expression<Func<TModel, bool>> predicate)
            where TModel : class, IModel;
    }
}
