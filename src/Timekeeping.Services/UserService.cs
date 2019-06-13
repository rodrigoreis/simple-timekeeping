using System.Threading.Tasks;
using Timekeeping.Repositories.Abstractions;
using Timekeeping.Repositories.Abstractions.Models;
using Timekeeping.Services.Abstractions;
using Timekeeping.Services.Abstractions.Dtos;

namespace Timekeeping.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserModel> _repository;

        public UserService(IRepository<UserModel> repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> SaveAsync(UserDto user)
        {
            var userModel = await _repository.SaveAsync(new UserModel
            {
                Name = user.Name,
                Email = user.Email
            });

            return new UserDto
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email
            };
        }
    }
}
