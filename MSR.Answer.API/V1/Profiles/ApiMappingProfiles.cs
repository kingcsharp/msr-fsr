using AutoMapper;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;

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
            CreateMap<GetUsersRequest, GetUsers>();
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
                .ForMember(dest => dest.Target, opts => opts.MapFrom(src => Convert.ToSingle(src.TargetValue)))
                .ForMember(dest => dest.SendNCREmail, opts => opts.MapFrom(src => src.SendEmailNotification));
            CreateMap<UpdateProcedureStepMonitorRequest, UpdateProcedureStepMonitor>()
                .ForMember(dest => dest.FailAction, opts => opts.MapFrom(src => src.FaultHandling))
                .ForMember(dest => dest.Target, opts => opts.MapFrom(src => Convert.ToSingle(src.TargetValue)))
                .ForMember(dest => dest.SendNCREmail, opts => opts.MapFrom(src => src.SendEmailNotification));
            CreateMap<CreateProcedureStepTemplateRequest, CreateProcedureStepTemplate>()
                .ForMember(dest => dest.SystemTaskId, opts => opts.MapFrom(src => src.ProcedureStepTypeId))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.StepText));
            CreateMap<UpdateProcedureStepTemplateRequest, UpdateProcedureStepTemplate>()
                .ForMember(dest => dest.SystemTaskId, opts => opts.MapFrom(src => src.ProcedureStepTypeId))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.StepText));
            CreateMap<CreateProcedureTemplateRequest, CreateProcedureStepTemplate>();
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
            CreateMap<GetPurchasesRequest, GetPurchases>();
            CreateMap<CreatePurchaseRequest, CreatePurchase>();
            CreateMap<GetPurchaseOrderRequest, GetPurchaseOrder>();
            CreateMap<CreateQuoteRequest, CreateQuote>();
            CreateMap<CreateQuoteItemRequest, CreateQuoteItem>();
            CreateMap<GetQuoteRequest, GetQuote>();
            CreateMap<CreateProductRequest, CreateProduct>();
            CreateMap<GetProductRequest, GetProduct>();
            CreateMap<UpdateProductRequest, UpdateProduct>();
            CreateMap<CreateRoleRequest, CreateRole>();
            CreateMap<UpdateRoleRequest, UpdateRole>();
            CreateMap<CreatePurchaseOrderRequest, CreatePurchaseOrder>();
            CreateMap<UpdatePurchaseOrderRequest, UpdatePurchaseOrder>();
            CreateMap<GetWorkOrderStatus, GetWorkOrder>();
            CreateMap<WorkOrderModel, WorkOrderGridSummary>()
                // SerialNumber (entered at purchase time, if any)
                .ForMember(dest => dest.SerialNumber, opts => opts.MapFrom(src => src.Purchase != null ? src.Purchase.SerialNumber : ""))
                // PurchaseOrderNumber
                .ForMember(dest => dest.PurchaseOrderNumber, opts => opts.MapFrom(src =>
                    src.Purchase != null ?
                        src.Purchase.PurchaseOrder != null ?
                            src.Purchase.PurchaseOrder.Id : 0
                        : 0))
                // Quantity
                .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.Purchase != null ? src.Purchase.Qty : 0));
            CreateMap<WorkOrderModel, WorkOrderStatus>()
                .ForMember(dest => dest.ProductName, opts => opts.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.LocationName, opts => opts.MapFrom(src => src.Location.Name));
            CreateMap<UpdateWorkOrderPartRequest, UpdateWorkOrderPart>();
            CreateMap<CreateWorkOrderTaskRequest, CreateWorkOrderTask>();
            CreateMap<UpdateWorkOrderTaskRequest, UpdateWorkOrderTask>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.WorkOrderTaskId))
                .ForMember(dest => dest.AssignedTo, opts => opts.MapFrom(src => src.AssignedUserId))
                .ForMember(dest => dest.TaskRunningSince, opts => opts.MapFrom(src =>
                    (src.TaskRunningSince.Ticks > 0) ? src.TaskRunningSince : (DateTime?)null));
            CreateMap<UpdateWorkOrderTaskMonitorRequest, UpdateWorkOrderTaskMonitor>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.WorkOrderTaskMonitorId));

            CreateMap<GetSearchRequest, GetSearch>();
            CreateMap<GetDashboardRequest, GetDashboard>();
            CreateMap<UpdateAdminCostSettingRequest, UpdateAdminCostSetting>();
            CreateMap<GetEquipmentMaintenanceRequest, GetEquipmentMaintenance>();
            CreateMap<CreateEquipmentMaintenanceRequest, CreateEquipmentMaintenance>();
            CreateMap<UpdateEquipmentMaintenanceRequest, UpdateEquipmentMaintenance>();
            CreateMap<GetDocumentRequest, GetDocument>();
            CreateMap<CreateDocumentRequest, CreateDocument>();
            CreateMap<UpdateDocumentRequest, UpdateDocument>();
        }
    }
}
