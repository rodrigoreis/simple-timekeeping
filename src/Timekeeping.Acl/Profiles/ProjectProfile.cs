using AutoMapper;
using Timekeeping.Repositories.Abstractions.Models;
using Timekeeping.Services.Abstractions.Dtos;

namespace Timekeeping.Acl.Profiles
{
    public class ProjectDtoProfile : Profile
    {
        public ProjectDtoProfile()
        {
            CreateMap<ProjectModel, ProjectModel>();

            CreateMap<ProjectDto, ProjectDto>();

            CreateMap<ProjectDto, ProjectModel>()
                .ReverseMap();

            //CreateMap<Expression<Func<ProjectDto, bool>>, Expression<Func<ProjectModel, bool>>>()
            //    .ReverseMap();
        }
    }
}
