using AutoMapper;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            CreateMap<CreateSupportRequest, CreateSupport>();
            CreateMap<CreateHelpPageRequest, CreateHelpPage>();
            CreateMap<CreateHelpPageRoleRequest, CreateHelpPageRole>();
            CreateMap<UpdateHelpPageRequest, UpdateHelpPage>();
            CreateMap<GetHelpPageRequest, GetHelpPage>();
        }
    }
}
