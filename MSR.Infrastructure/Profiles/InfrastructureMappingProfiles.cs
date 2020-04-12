using AutoMapper;

namespace MSR.Infrastructure.Profiles
{
    public class InfrastructureMappingProfiles: Profile
    {
        public InfrastructureMappingProfiles()
        {
            CreateMap<Resources.EntityFramework.Entities.User, Domain.Models.User>();
        }
    }
}
