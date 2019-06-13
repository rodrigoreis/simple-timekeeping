using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Timekeeping.Repositories.Contexts;

namespace Timekeeping.UnitTests.Fixture
{
    public class TestServerFixture<TStartup> where TStartup : class
    {
        private TestServer _testServer;

        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureAppConfiguration { get; set; }

        public Action<IApplicationBuilder> Configure { get; set; }

        public Action<WebHostBuilderContext, IServiceCollection> ConfigureServices { get; set; }

        public TestServerFixture()
        {
            ConfigureAppConfiguration = delegate
            {
            };
            ConfigureServices = delegate
            {
            };
            Configure = delegate
            {
            };
        }

        public T Resolve<T>()
        {
            return ((_testServer ?? (_testServer = new TestServer(CreateBuilder()))).Host.Services).GetService<T>();
        }

        private WebHostBuilder CreateBuilder()
        {
            var webHostBuilder = new WebHostBuilder();

            webHostBuilder
                .UseEnvironment("Test")
                .UseStartup(typeof(TStartup))
                .ConfigureAppConfiguration(delegate (WebHostBuilderContext context, IConfigurationBuilder b)
                {
                    b.SetBasePath(Directory.GetCurrentDirectory());
                    b.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    b.AddEnvironmentVariables();
                    ConfigureAppConfiguration(context, b);
                })
                .ConfigureServices(ConfigureServices)
                .Configure(Configure);

            return webHostBuilder;
        }

        public TimekeepingContext BuildInMemoryDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TimekeepingContext>()
                .UseSqlite(connection)
                .Options;

            var context = new TimekeepingContext(options);

            context.Database.Migrate();

            return context;
        }

        public Func<Task> Method<TDomainModel>(Func<TDomainModel, Task> func, TDomainModel arg)
        {
            return async () => await func.Invoke(arg);
        }

        public Func<Task> Method(Func<Task> func)
        {
            return async () => await func.Invoke();
        }
    }
}
