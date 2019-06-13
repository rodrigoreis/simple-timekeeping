using System.Threading.Tasks;
using Timekeeping.Services.Abstractions.Dtos;

namespace Timekeeping.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserDto> SaveAsync(UserDto user);
    }
}
