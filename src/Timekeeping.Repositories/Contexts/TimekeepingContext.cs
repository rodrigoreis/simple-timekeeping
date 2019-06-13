using Microsoft.EntityFrameworkCore;

namespace Timekeeping.Repositories.Contexts
{
    public class TimekeepingContext : DbContext
    {
        public TimekeepingContext(DbContextOptions<TimekeepingContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TimekeepingContext).Assembly);
        }
    }
}
