using FluentAssertions;
using System;
using Timekeeping.Repositories;
using Timekeeping.Services;
using Timekeeping.Services.Abstractions;
using Timekeeping.Services.Abstractions.Dtos;
using Timekeeping.Ui;
using Timekeeping.UnitTests.Fixture;
using Xunit;

namespace Timekeeping.UnitTests
{
    public class UserTest : IClassFixture<TestServerFixture<Startup>>
    {
        private TestServerFixture<Startup> _fixture;
        private readonly IUserService _userService;

        public UserTest(TestServerFixture<Startup> fixture)
        {
            _fixture = fixture;
            _userService = new UserService(new UserRepository(_fixture.BuildInMemoryDbContext()));
        }

        [Fact]
        [Trait("User", "Should create an user")]
        public void ShouldCreateAnUser()
        {
            _fixture.Method(_userService.CreateUserAsync, new UserDto { Name = "Rodrigo Reis", Email = "rodrigo.reis@squadra.com.br" })
                .Should().NotThrow();
        }

        [Fact]
        [Trait("User", "Should edit an user")]
        public void ShouldEditAnUser()
        {
            throw new NotImplementedException();
        }

        [Fact]
        [Trait("User", "Should delete an user")]
        public void ShouldDeleteAnUser()
        {
            throw new NotImplementedException();
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
