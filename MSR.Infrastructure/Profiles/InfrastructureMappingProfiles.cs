using AutoMapper;
using MSR.Domain.Commands;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Profiles
{
    public class InfrastructureMappingProfiles : Profile
    {
        public InfrastructureMappingProfiles()
        {
            CreateMap<User, Domain.Models.User>()
                .ForMember(dest => dest.Roles, opts => opts.Ignore())
                .ReverseMap();
            CreateMap<CreateUser, Resources.EntityFramework.Entities.User>();
            CreateMap<UpdateUser, Resources.EntityFramework.Entities.User>();
            CreateMap<Resources.EntityFramework.Entities.Customer, Domain.Models.Customer>().ReverseMap();
            CreateMap<Resources.EntityFramework.Entities.Location, Domain.Models.Location>().ReverseMap();
            CreateMap<Resources.EntityFramework.Entities.TimeZone, Domain.Models.TimeZone>().ReverseMap();
            CreateMap<GetLocations, Location>(); 
            CreateMap<Role, Domain.Models.Role>()
                .ForMember(dest => dest.Menus, opt => opt.Ignore());
            CreateMap<Location, Domain.Models.Location>();

            /*Workflow*/
            CreateMap<CreateWorkflowGroupModel, WorkflowGroup>();
            CreateMap<UpdateWorkflowGroupModel, WorkflowGroup>();
            CreateMap<Domain.Models.WorkflowGroupRoleMapModel, WorkflowGroupRoleMap>();

            CreateMap<WorkflowGroup, Domain.Models.WorkflowGroupModel>();
            CreateMap<WorkflowGroupRoleMap, Domain.Models.WorkflowGroupRoleMapModel>();

            CreateMap<Domain.Models.Customer, Customer>().ReverseMap();
            CreateMap<Domain.Models.Customer, CustomerApproval>().ReverseMap();                          
            CreateMap<UpdateMenuRoleMap, MenuRolePermission>();
        }
    }
}
