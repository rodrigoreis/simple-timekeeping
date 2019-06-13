using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Timekeeping.Repositories.Contexts;

namespace Timekeeping.Repositories
{
    public class DbContextFactory : IDesignTimeDbContextFactory<TimekeepingContext>
    {
        public TimekeepingContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();

            if (args != null)
                builder
                    .AddCommandLine(args);

            var config = builder.Build();

            var options = new DbContextOptionsBuilder<TimekeepingContext>()
                .UseSqlite(config.GetConnectionString("Default"))
                .Options;

            return new TimekeepingContext(options);
        }
    }
}
