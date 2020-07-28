using AutoMapper;
using Microsoft.VisualBasic.CompilerServices;
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
                .ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.GetFullName()))
                .ReverseMap();
            CreateMap<CreateUser, User>();
            CreateMap<UpdateUser, User>();

            CreateMap<Customer, Customer>();


            CreateMap<Customer, Domain.Models.Customer>().ReverseMap();
            CreateMap<Location, Domain.Models.LocationModel>().ReverseMap();
            CreateMap<TimeZone, Domain.Models.TimeZone>().ReverseMap();
            CreateMap<GetLocations, Location>();
            CreateMap<User, UserApproval>();

            CreateMap<GetLocations, Location>();

            CreateMap<Role, Domain.Models.Role>()
                .ForMember(dest => dest.Menus, opt => opt.Ignore()).ReverseMap();
            CreateMap<Location, Domain.Models.LocationModel>();

            /*Workflow*/
            CreateMap<CreateWorkflowGroupModel, WorkflowGroup>();
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

            CreateMap<Domain.Models.LocationModel, Location>().ReverseMap();
            CreateMap<Domain.Models.LocationModel, LocationApproval>().ReverseMap();
            CreateMap<CreateLocation, LocationApproval>();
            CreateMap<CreateLocation, Location>();

            CreateMap<UpdateLocation, LocationApproval>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateLocation, Location>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<HelpPage, Domain.Models.HelpPage>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<Domain.Models.Customer, CustomerApproval>().ReverseMap();
            CreateMap<UpdateMenuRoleMap, MenuRolePermission>();

            #region Part
            CreateMap<Part, Domain.Models.PartModel>()
                .ForMember(dest => dest.CreateSubParts, opt => opt.MapFrom(src => src.Subparts))
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Created.GetFullName()))
                .ForMember(dest => dest.LastUpdatedByName, opt => opt.MapFrom(src => src.LastUpdated.GetFullName()));


            CreateMap<PartSubPartMap, Domain.Models.SubPartModel>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentPartId))
                .ForMember(dest => dest.PartId, opt => opt.MapFrom(src => src.PartId))
                .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty));


            CreateMap<CreatePart, PartApproval>();
            CreateMap<CreatePart, Part>().ForMember("Subparts", opts => opts.Ignore());
            CreateMap<Domain.Models.SubPartModel, PartSubPartMap>();
            CreateMap<UpdatePart, PartApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.PartId, opts => opts.MapFrom(src => src.Id));
            CreateMap<UpdatePart, Part>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region Procedure
            CreateMap<Procedure, Domain.Models.Procedure>();
            CreateMap<ProcedureStep, Domain.Models.ProcedureStep>();
            CreateMap<ProcedureStepTemplate, Domain.Models.ProcedureStepTemplate>();
            CreateMap<ProcedureType, Domain.Models.ProcedureType>();
            CreateMap<CreateProcedure, ProcedureApproval>();
            CreateMap<CreateProcedure, Procedure>();
            CreateMap<CreateProcedureStep, ProcedureStepApproval>();
            CreateMap<CreateProcedureStep, ProcedureStep>();
            CreateMap<UpdateProcedure, ProcedureApproval>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateProcedure, Procedure>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(0)));
            CreateMap<UpdateProcedureStep, ProcedureStepApproval>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateProcedureStep, ProcedureStep>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(0)));
            CreateMap<CreateProcedureStepTemplate, ProcedureStepTemplate>();
            CreateMap<UpdateProcedureStepTemplate, ProcedureStepTemplate>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(0)));
            CreateMap<CreateProcedureType, ProcedureType>();
            CreateMap<UpdateProcedureType, ProcedureType>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(0)));
            #endregion

            // Monitor
            CreateMap<ProcedureStepMonitor, Domain.Models.ProcedureStepMonitor>();
            CreateMap<MonitorInputType, Domain.Models.ProcedureStepMonitorInputType>()
                .ForMember(dest => dest.InputTypeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InputTypeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MonitorTypeName, opt => opt.MapFrom(src => src.Type.Name));
            CreateMap<MonitorListItem, Domain.Models.ProcedureStepMonitorListItem>()
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ListItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ListName, opt => opt.MapFrom(src => src.List.Name));
            CreateMap<CreateProcedureStepMonitor, ProcedureStepMonitor>();
            CreateMap<UpdateProcedureStepMonitor, ProcedureStepMonitor>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(0)));

            #region EntityApprovalToEntity
            CreateMap<UserApproval, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CustomerApproval, Customer>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<DocumentApproval, Document>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<LocationApproval, Location>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<PartApproval, Part>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ProcedureApproval, Procedure>().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProcedureSteps, opt => opt.MapFrom(src => src.ProcedureStepApprovals));
            CreateMap<ProcedureStepApproval, ProcedureStep>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ProductApproval, Product>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<PurchaseOrderApproval, PurchaseOrder>().ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion


            CreateMap<MenuRolePermission, Domain.Models.Permission>().ReverseMap();

            CreateMap<MenuItem, Domain.Models.MenuItem>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()).ReverseMap();
            CreateMap<MenuGroup, Domain.Models.MenuGroup>().ReverseMap();

            CreateMap<Status, Domain.Models.Status>().ReverseMap();
        }
    }
}
