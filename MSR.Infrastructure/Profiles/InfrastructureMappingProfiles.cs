using AutoMapper;
using MSR.Domain.Commands;

namespace MSR.Infrastructure.Profiles
{
    public class InfrastructureMappingProfiles: Profile
    {
        public InfrastructureMappingProfiles()
        {
            CreateMap<Resources.EntityFramework.Entities.User, Domain.Models.User>()
                .ForMember(dest => dest.Roles, opts => opts.Ignore())
                .ReverseMap();
            CreateMap<CreateUser, Resources.EntityFramework.Entities.User>();
            CreateMap<UpdateUser, Resources.EntityFramework.Entities.User>();
            CreateMap<Resources.EntityFramework.Entities.Customer, Domain.Models.Customer>().ReverseMap();
            CreateMap<Resources.EntityFramework.Entities.Location, Domain.Models.Location>().ReverseMap();
            CreateMap<Resources.EntityFramework.Entities.TimeZone, Domain.Models.TimeZone>().ReverseMap();
        }
    }
}
