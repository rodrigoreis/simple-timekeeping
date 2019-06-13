using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Timekeeping.Services.Abstractions
{
    public interface IDtoService<TDto>
        where TDto : class, IDto
    {
        Task<IQueryable<TDto>> GetQueryableAsync(params string[] expands);
        Task<IEnumerable<TDto>> GetAllAsync(params string[] expands);
        Task<TDto> GetAsync(Expression<Func<TDto, bool>> predicate, params string[] expands);
        Task AddAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task DeleteAsync(TDto dto);
    }
}
