using AutoMapper;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commands;

namespace MSR.Answer.API.V1.Profiles
{
    public class ApiMappingProfiles: Profile
    {
        public ApiMappingProfiles()
        {
            CreateMap<CreateLocationRequest, CreateLocation>();
            CreateMap<UpdateLocationRequest, UpdateLocation>();
            CreateMap<CreateUserRoleRequest, CreateUserRole>();
            CreateMap<UpdateUserRoleRequest, UpdateUserRole>();
            CreateMap<CreateHelpPageRequest, CreateHelpPage>();
            CreateMap<CreateHelpPageRoleRequest, CreateHelpPageRole>();
            CreateMap<UpdateHelpPageRequest, UpdateHelpPage>();
            CreateMap<GetHelpPageRequest, GetHelpPage>();
        }
    }
}
