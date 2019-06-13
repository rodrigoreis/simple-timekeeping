using AutoMapper;
using Timekeeping.Repositories.Abstractions.Models;
using Timekeeping.Services.Abstractions.Dtos;

namespace Timekeeping.Acl.Profiles
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<UserModel, UserModel>();

            CreateMap<UserDto, UserDto>();

            CreateMap<UserDto, UserModel>()
                .ReverseMap();

            //CreateMap<Expression<Func<UserDto, bool>>, Expression<Func<UserModel, bool>>>()
            //    .ReverseMap();
        }
    }
}
