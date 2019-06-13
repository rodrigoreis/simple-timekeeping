using AutoMapper;
using Timekeeping.Repositories.Abstractions;
using Timekeeping.Repositories.Abstractions.Models;
using Timekeeping.Services.Abstractions;
using Timekeeping.Services.Abstractions.Dtos;

namespace Timekeeping.Services
{
    public class UserService : DtoService<UserModel, UserDto>, IDtoService<UserDto>
    {
        public UserService(IMapper mapper, IModelRepository<UserModel> repository) : base(mapper, repository)
        { }
    }
}
