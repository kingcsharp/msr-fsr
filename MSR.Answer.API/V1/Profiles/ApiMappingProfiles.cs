using AutoMapper;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;
using System.Configuration;
using System.Linq;

namespace MSR.Answer.API.V1.Profiles
{
    /// <summary>
    ///
    /// </summary>
    public class ApiMappingProfiles: Profile
    {
        /// <summary>
        ///
        /// </summary>
        public ApiMappingProfiles()
        {
            CreateMap<GetLocationRequest, GetLocations>();
            CreateMap<CreateLocationRequest, CreateLocation>();
            CreateMap<UpdateLocationRequest, UpdateLocation>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.LocationId));
            CreateMap<CreateUserRoleRequest, CreateUserRole>();
            CreateMap<UpdateUserRoleRequest, UpdateUserRole>();
            CreateMap<RoleRequest, Role>()
                .ForMember(dest => dest.Menus, opts => opts.Ignore()); // <- FIXME
            CreateMap<MenuItemRequest, MenuItem>();
            CreateMap<CreateHelpPageRequest, CreateHelpPage>();
            CreateMap<CreateHelpPageRoleRequest, CreateHelpPageRole>();
            CreateMap<UpdateHelpPageRequest, UpdateHelpPage>();
            CreateMap<GetHelpPageRequest, GetHelpPage>();

            CreateMap<CreatePartRequest, CreatePart>()
            .ForMember(dest => dest.SubParts, opts => opts.MapFrom(src => src.CreateSubParts));
            CreateMap<UpdatePartRequest, UpdatePart>()
                .ForMember(dest=>dest.SubParts, opts => opts.MapFrom(src => src.CreateSubParts));

            CreateMap<CreateProcedureRequest, CreateProcedure>();
            CreateMap<UpdateProcedureRequest, UpdateProcedure>();
            CreateMap<CreateProcedureStepRequest, CreateProcedureStep>()
                .ForMember(dest => dest.StepText, opts => opts.MapFrom(src => src.Text));
            CreateMap<UpdateProcedureStepRequest, UpdateProcedureStep>()
                .ForMember(dest => dest.StepText, opts => opts.MapFrom(src => src.Text));
            CreateMap<CreateProcedureStepMonitorRequest, CreateProcedureStepMonitor>()
                .ForMember(dest => dest.FailAction, opts => opts.MapFrom(src => src.FaultHandling))
                .ForMember(dest => dest.MonitorTypeId, opts => opts.MapFrom(src => Int32.Parse(src.InputType)))
                .ForMember(dest => dest.ShouldBe, opts => opts.MapFrom(src => src.TargetValue))
                .ForMember(dest => dest.InputTypeId, opts => opts.MapFrom(src => Int32.Parse(src.InputType)));
            CreateMap<UpdateProcedureStepMonitorRequest, UpdateProcedureStepMonitor>()
                .ForMember(dest => dest.FailAction, opts => opts.MapFrom(src => src.FaultHandling))
                .ForMember(dest => dest.MonitorTypeId, opts => opts.MapFrom(src => Int32.Parse(src.InputType)))
                .ForMember(dest => dest.ShouldBe, opts => opts.MapFrom(src => src.TargetValue))
                .ForMember(dest => dest.InputTypeId, opts => opts.MapFrom(src => Int32.Parse(src.InputType)));
            CreateMap<CreateProcedureStepTemplateRequest, CreateProcedureStepTemplate>();
            CreateMap<UpdateProcedureStepTemplateRequest, UpdateProcedureStepTemplate>();
            CreateMap<CreateProcedureTemplateRequest, CreateProcedureStepTemplate>()
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => String.Join(',',src.Roles.Select(y => y.Id).ToList())))
                .ForMember(dest => dest.StepText, opts => opts.MapFrom(src => src.Text));
            CreateMap<UpdateProcedureTemplateRequest, UpdateProcedureStepTemplate>();
            CreateMap<CreateProcedureTypeRequest, CreateProcedureType>();
            CreateMap<UpdateProcedureTypeRequest, UpdateProcedureType>();
            CreateMap<CreateWorkOrderRequest, CreateWorkOrder>().ReverseMap();
            CreateMap<DeleteWorkOrderRequest, DeleteWorkOrder>().ReverseMap();
            CreateMap<GetWorkOrderRequest, GetWorkOrder>();
            CreateMap<UpdateWorkOrderRequest, UpdateWorkOrder>()
                .ForMember(dest => dest.LocationId, opts => opts.Condition(src => src.LocationId > 0))
                .ForMember(dest => dest.ProductId, opts => opts.Condition(src => src.ProductId > 0))
                .ForMember(dest => dest.PurchaseId, opts => opts.Condition(src => src.PurchaseId > 0))
                .ForMember(dest => dest.Price, opts => opts.Condition(src => src.Price > 0));
            CreateMap<WorkOrderTaskMonitorRequest, WorkOrderTaskMonitorModel>().ReverseMap();
            CreateMap<WorkOrderPartRequest, WorkOrderPartModel>().ReverseMap();
            CreateMap<WorkOrderTaskRequest, WorkOrderTaskModel>().ReverseMap();
            CreateMap<DeleteMenuRoleMapRequest, RemoveMenuRoleMap>();
            CreateMap<FileRequest, FileModel>().ReverseMap();
            CreateMap<CreateCustomerRequest, CreateCustomer>();
            CreateMap<UpdateCustomerRequest, UpdateCustomer>();
            CreateMap<CreateFileRequest, CreateFile>();
            CreateMap<UploadFileRequest, UploadFile>();
            CreateMap<GetInvoicesRequest, GetInvoicesGridView>();
            CreateMap<GetInvoicesRequest, GetInvoices>();
            CreateMap<DownloadInvoicesRequest, DownloadAsIIFInvoices>();
            CreateMap<CreateInvoiceRequest, CreateOneInvoice>();
            CreateMap<CreateInvoiceItemRequest, CreateUpdateInvoiceItem>();
            CreateMap<ImportRequest, ImportFile>();
            CreateMap<GetSensorRequest, GetSensor>();
        }
    }
}
