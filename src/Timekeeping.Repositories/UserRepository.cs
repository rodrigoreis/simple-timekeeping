using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timekeeping.Repositories.Abstractions;
using Timekeeping.Repositories.Abstractions.Models;
using Timekeeping.Repositories.Contexts;

namespace Timekeeping.Repositories
{
    public class UserRepository : IRepository<UserModel>
    {
        private readonly TimekeepingContext _context;

        public UserRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserModel> SaveAsync(UserModel model)
        {
            var existing = await _context.Set<UserModel>().SingleOrDefaultAsync(
                    m => m.Id == model.Id
                );

            if (existing != null)
            {
                _context.Update(existing);
            }
            else
            {
                _context.Add(model);
            }

            await _context.SaveChangesAsync();

            return model;
        }
    }
}
