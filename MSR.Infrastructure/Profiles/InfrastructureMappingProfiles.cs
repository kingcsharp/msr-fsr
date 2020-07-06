using AutoMapper;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
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
            CreateMap<CreateUser, User>();
            CreateMap<UpdateUser, User>();
            CreateMap<Customer, Domain.Models.Customer>().ReverseMap();
            CreateMap<Location, Domain.Models.Location>().ReverseMap();
            CreateMap<TimeZone, Domain.Models.TimeZone>().ReverseMap();
            CreateMap<GetLocations, Location>(); 
            CreateMap<User, UserApproval>();

            CreateMap<GetLocations, Location>();

            CreateMap<Role, Domain.Models.Role>()
                .ForMember(dest => dest.Menus, opt => opt.Ignore()).ReverseMap();
            CreateMap<Location, Domain.Models.Location>();

            /*Workflow*/
            CreateMap<CreateWorkflowGroup, WorkflowGroup>();
            CreateMap<UpdateWorkflowGroupModel, WorkflowGroup>();
            CreateMap<Domain.Models.WorkflowGroupRoleMapModel, WorkflowGroupRoleMap>();

            CreateMap<WorkflowGroup, Domain.Models.WorkflowGroupModel>();
            CreateMap<WorkflowGroupRoleMap, Domain.Models.WorkflowGroupRoleMapModel>();

            CreateMap<Domain.Models.Customer, Customer>().ReverseMap();
            CreateMap<Domain.Models.Customer, CustomerApproval>().ReverseMap();
            CreateMap<CreateCustomer, Customer>();
            CreateMap<UpdateCustomer, Customer>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateCustomer, CustomerApproval>();
            CreateMap<UpdateCustomer, CustomerApproval>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Domain.Models.Location, Location>().ReverseMap();
            CreateMap<Domain.Models.Location, LocationApproval>().ReverseMap();
            CreateMap<CreateLocation, LocationApproval>();
            CreateMap<CreateLocation, Location>();

            CreateMap<UpdateLocation, LocationApproval>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateLocation, Location>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<HelpPage, Domain.Models.HelpPage>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<Domain.Models.Customer, CustomerApproval>().ReverseMap();                          
            CreateMap<UpdateMenuRoleMap, MenuRolePermission>();
        }
    }
}
