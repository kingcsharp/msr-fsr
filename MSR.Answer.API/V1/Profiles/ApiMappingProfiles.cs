using AutoMapper;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commands;

namespace MSR.Answer.API.V1.Profiles
{
    public class ApiMappingProfiles: Profile
    {
        public ApiMappingProfiles()
        {
            CreateMap<GetLocationRequest, GetLocations>();
            CreateMap<CreateLocationRequest, CreateLocation>();
            CreateMap<UpdateLocationRequest, UpdateLocation>();
            CreateMap<CreateUserRoleRequest, CreateUserRole>();
            CreateMap<UpdateUserRoleRequest, UpdateUserRole>();
            CreateMap<CreateHelpPageRequest, CreateHelpPage>();
            CreateMap<CreateHelpPageRoleRequest, CreateHelpPageRole>();
            CreateMap<UpdateHelpPageRequest, UpdateHelpPage>();
            CreateMap<GetHelpPageRequest, GetHelpPage>();

            CreateMap<CreatePartRequest, CreatePart>()
            .ForMember(dest => dest.SubParts, opts => opts.MapFrom(src => src.CreateSubParts));
            CreateMap<UpdatePartRequest, UpdatePart>()
                .ForMember(dest=>dest.SubParts, opts => opts.MapFrom(src=>src.CreateSubParts));

            CreateMap<CreateProcedureRequest, CreateProcedure>();
            CreateMap<UpdateProcedureRequest, UpdateProcedure>();
            CreateMap<CreateProcedureStepRequest, CreateProcedureStep>();
            CreateMap<UpdateProcedureStepRequest, UpdateProcedureStep>();
            CreateMap<CreateProcedureStepMonitorRequest, CreateProcedureStepMonitor>();
            CreateMap<UpdateProcedureStepMonitorRequest, UpdateProcedureStepMonitor>();
            CreateMap<CreateProcedureStepTemplateRequest, CreateProcedureStepTemplate>();
            CreateMap<UpdateProcedureStepTemplateRequest, UpdateProcedureStepTemplate>();
            CreateMap<CreateProcedureTypeRequest, CreateProcedureType>();
            CreateMap<UpdateProcedureTypeRequest, UpdateProcedureType>();
        }
    }
}
