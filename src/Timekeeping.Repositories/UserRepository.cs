using Timekeeping.Repositories.Abstractions;
using Timekeeping.Repositories.Abstractions.Models;
using Timekeeping.Repositories.Contexts;

namespace Timekeeping.Repositories
{
    public class UserRepository : ModelRepository<UserModel, TimekeepingContext>, IModelRepository<UserModel>
    {
        public UserRepository(TimekeepingContext context) : base(context)
        { }
    }
}
