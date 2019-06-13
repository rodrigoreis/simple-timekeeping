using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Timekeeping.Repositories.Abstractions;

namespace Timekeeping.Repositories
{
    public class ModelRepository<TModel, TDbContext> : Repository<TDbContext>, IModelRepository<TModel>
        where TModel : class, IModel
        where TDbContext : DbContext
    {
        public ModelRepository(TDbContext context) : base(context) { }

        public Task AddAsync(TModel model, Expression<Func<TModel, bool>> predicate) =>
            base.AddAsync(model, predicate);

        public Task DeleteAsync(TModel model, Expression<Func<TModel, bool>> predicate) =>
            base.DeleteAsync(model, predicate);

        public Task<IEnumerable<TModel>> GetAllAsync(params string[] expands) =>
            GetAllAsync<TModel>(expands);

        public Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate, params string[] expands) =>
            base.GetAsync(predicate, expands);

        public Task<IQueryable<TModel>> GetQueryableAsync(params string[] expands) =>
            GetQueryableAsync<TModel>(expands);

        public Task UpdateAsync(TModel model, Expression<Func<TModel, bool>> predicate) =>
            base.UpdateAsync(model, predicate);
    }
}
