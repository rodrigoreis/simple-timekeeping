using AutoMapper;
using Timekeeping.Repositories.Abstractions.Models;
using Timekeeping.Services.Abstractions.Dtos;

namespace Timekeeping.Acl.Profiles
{
    public class TimeEntryDtoProfile : Profile
    {
        public TimeEntryDtoProfile()
        {
            CreateMap<TimeEntryModel, TimeEntryModel>();

            CreateMap<TimeEntryDto, TimeEntryDto>();

            CreateMap<TimeEntryDto, TimeEntryModel>()
                .ReverseMap();

            //CreateMap<Expression<Func<TimeEntryDto, bool>>, Expression<Func<TimeEntryModel, bool>>>()
            //    .ReverseMap();
        }
    }
}
