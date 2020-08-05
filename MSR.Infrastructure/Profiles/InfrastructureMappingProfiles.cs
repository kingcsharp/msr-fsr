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
            #region User
            CreateMap<User, Domain.Models.User>()
                .ForMember(dest => dest.Roles, opts => opts.Ignore())
                .ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.GetFullName()))
                .ReverseMap();
            CreateMap<CreateUser, User>();
            CreateMap<UpdateUser, User>();
            CreateMap<User, UserApproval>();
            #endregion

            #region Customer 
            CreateMap<Customer, Domain.Models.Customer>().ReverseMap()
                    .ForMember(dest => dest.Location, opts => opts.AllowNull())
                    .ForMember(dest => dest.PrimaryContactUser, opts => opts.AllowNull())
                    .ForMember(dest => dest.SecondaryContactUser, opts => opts.AllowNull())
                    .AfterMap((src, dest) => dest.Location = src.Location == null ? null : dest.Location)
                    .AfterMap((src, dest) => dest.PrimaryContactUser = src.PrimaryContactUser == null ? null : dest.PrimaryContactUser)
                    .AfterMap((src, dest) => dest.SecondaryContactUser = src.SecondaryContactUser == null ? null : dest.SecondaryContactUser);
            CreateMap<Domain.Models.Customer, Customer>().ReverseMap();
            CreateMap<CustomerApproval, Domain.Models.Customer>()
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name));
            CreateMap<CreateCustomer, Customer>();
            CreateMap<CreateCustomer, CustomerApproval>();
            CreateMap<Customer, CustomerApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            CreateMap<UpdateCustomer, Customer>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateCustomer, CustomerApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region Location
            CreateMap<Location, Domain.Models.LocationModel>().ReverseMap();
            CreateMap<GetLocations, Location>();
            CreateMap<Domain.Models.LocationModel, Location>().ReverseMap();
            CreateMap<Domain.Models.LocationModel, LocationApproval>();
            CreateMap<LocationApproval, Domain.Models.LocationModel>()
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name));
            CreateMap<CreateLocation, LocationApproval>();
            CreateMap<CreateLocation, Location>();
            CreateMap<LocationApproval, Location>().ReverseMap()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateLocation, LocationApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateLocation, Location>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            CreateMap<TimeZone, Domain.Models.TimeZone>().ReverseMap();
            CreateMap<Invoice, Domain.Models.InvoiceModel>().ReverseMap();
            CreateMap<InvoiceItem, Domain.Models.InvoiceItemModel>().ReverseMap();
           

            CreateMap<Role, Domain.Models.Role>()
                .ForMember(dest => dest.Menus, opt => opt.Ignore()).ReverseMap();

            /*Workflow*/
            CreateMap<CreateWorkflowGroupModel, WorkflowGroup>();
            CreateMap<UpdateWorkflowGroupModel, WorkflowGroup>();
            CreateMap<Domain.Models.WorkflowGroupRoleMapModel, WorkflowGroupRoleMap>();

            CreateMap<WorkflowGroup, Domain.Models.WorkflowGroupModel>();
            CreateMap<WorkflowGroupRoleMap, Domain.Models.WorkflowGroupRoleMapModel>();

          

            CreateMap<Domain.Models.InvoiceModel, Invoice>().ReverseMap();
            CreateMap<CreateOneInvoice, Invoice>();
            CreateMap<CreateUpdateInvoiceItem, InvoiceItem>();
            CreateMap<UpdateInvoice, Invoice>();
            CreateMap<DownloadAsIIFInvoices, GetInvoices>();

           

            CreateMap<HelpPage, Domain.Models.HelpPage>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

           
            CreateMap<UpdateMenuRoleMap, MenuRolePermission>()
                .ForMember(dest => dest.Created, opts => opts.Ignore())
                .ForMember(dest => dest.CreatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdated, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdatedOn, opts => opts.Ignore());

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

            CreateMap<Status, Domain.Models.StatusModel>().ReverseMap();

            CreateMap<PartCSVRecord, UpdatePart>();
            CreateMap<PartCSVRecord, CreatePart>();

            CreateMap<UploadFile, Domain.Models.FileModel>();
        }
    }
}
