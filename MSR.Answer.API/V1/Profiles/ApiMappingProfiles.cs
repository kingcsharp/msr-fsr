using AutoMapper;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commands;
using MSR.Domain.Models;

namespace MSR.Answer.API.V1.Profiles
{
    public class ApiMappingProfiles: Profile
    {
        public ApiMappingProfiles()
        {
            CreateMap<GetLocationRequest, GetLocations>();
            CreateMap<CreateLocationRequest, CreateLocation>();
            CreateMap<UpdateLocationRequest, UpdateLocation>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.LocationId));
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
            CreateMap<File, FileModel>().ReverseMap();
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
            CreateMap<CreateQuoteRequest, CreateQuote>();
        }
    }
}
