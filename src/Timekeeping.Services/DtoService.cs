using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Timekeeping.Repositories.Abstractions;
using Timekeeping.Services.Abstractions;

namespace Timekeeping.Services
{
    public class DtoService<TModel, TDto> : IDtoService<TDto>
        where TModel : class, IModel
        where TDto : class, IDto
    {
        private readonly IModelRepository<TModel> _repository;
        private readonly IMapper _mapper;

        public DtoService(IMapper mapper, IModelRepository<TModel> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task AddAsync(TDto dto) =>
            _repository.AddAsync(_mapper.Map<TModel>(dto), m => m.Id.Equals(dto.Id));

        public Task DeleteAsync(TDto dto) =>
            _repository.DeleteAsync(_mapper.Map<TModel>(dto), m => m.Id.Equals(dto.Id));

        public Task<IEnumerable<TDto>> GetAllAsync(params string[] expands) =>
            Task.Run(() => _mapper.Map<IEnumerable<TDto>>(_repository.GetAllAsync(expands)));

        public async Task<TDto> GetAsync(Expression<Func<TDto, bool>> predicate, params string[] expands) =>
            _mapper.Map<TDto>(await _repository.GetAsync(_mapper.Map<Expression<Func<TModel, bool>>>(predicate)));

        public async Task<IQueryable<TDto>> GetQueryableAsync(params string[] expands) =>
            _mapper.ProjectTo<TDto>(await _repository.GetQueryableAsync(expands));

        public Task UpdateAsync(TDto dto) =>
            _repository.UpdateAsync(_mapper.Map<TModel>(dto), m => m.Id.Equals(dto.Id));

    }
}
