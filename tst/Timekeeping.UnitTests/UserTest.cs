using AutoMapper;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timekeeping.Repositories;
using Timekeeping.Services;
using Timekeeping.Services.Abstractions;
using Timekeeping.Services.Abstractions.Dtos;
using Timekeeping.Services.Acl;
using Timekeeping.Ui;
using Timekeeping.UnitTests.Fixture;
using Xunit;

namespace Timekeeping.UnitTests
{
    public class UserTest : IClassFixture<TestServerFixture<Startup>>
    {
        private readonly TestServerFixture<Startup> _fixture;
        private readonly IDtoService<UserDto> _userService;

        public UserTest(TestServerFixture<Startup> fixture)
        {
            _fixture = fixture;

            _fixture.ConfigureServices += (_, services) =>
            {
                services.AddAntiCorruptionLayer();
            };

            _userService = new UserService(_fixture.Resolve<IMapper>(), new UserRepository(_fixture.BuildInMemoryDbContext()));
        }

        [Fact]
        [Trait("User", "Should create an user")]
        public void ShouldCreateAnUser()
        {
            _fixture.Method(_userService.AddAsync, new UserDto { Name = "Rodrigo Reis", Email = "rodrigo.reis@squadra.com.br" })
                .Should().NotThrow();
        }

        [Fact]
        [Trait("User", "Should edit an user")]
        public async Task ShouldEditAnUser()
        {
            await _userService.AddAsync(new UserDto { Name = "Rodrigo Reis", Email = "rodrigo.reis@squadra.com.br" });
            var inserted = await _userService.GetAsync(u => u.Name.Equals("Rodrigo Reis") && u.Email.Equals("rodrigo.reis@squadra.com.br"));
            inserted.Name = $"{inserted.Name} - Edited";
            _fixture.Method(_userService.UpdateAsync, inserted)
                .Should().NotThrow();
        }

        [Fact]
        [Trait("User", "Should delete an user")]
        public async Task ShouldDeleteAnUser()
        {
            await _userService.AddAsync(new UserDto { Name = "Rodrigo Reis", Email = "rodrigo.reis@squadra.com.br" });
            var inserted = await _userService.GetAsync(u => u.Name.Equals("Rodrigo Reis") && u.Email.Equals("rodrigo.reis@squadra.com.br"));
            _fixture.Method(_userService.DeleteAsync, inserted)
                .Should().NotThrow();
        }

        [Fact]
        [Trait("User", "Should not create an user with same email")]
        public void ShouldNotCrateAnUserWithSameEmail()
        {
            throw new NotImplementedException();
        }

        [Fact]
        [Trait("User", "Should not edit an user with same email")]
        public void ShouldNotEditAnUserWithSameEmail()
        {
            throw new NotImplementedException();
        }
    }
}
