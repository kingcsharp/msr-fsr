using AutoMapper;
using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Models.Paging;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Models.Query;
using MSR.Domain.QueryFilters;
using System;
using System.Linq;

namespace MSR.Answer.API.V1.Profiles
{
    /// <summary>
    ///
    /// </summary>
    public class ApiMappingProfiles : Profile
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

            CreateMap<QueryRequestBase, QueryBase>();

            CreateMap<CreatePartRequest, CreatePart>()
            .ForMember(dest => dest.SubParts, opts => opts.MapFrom(src => src.CreateSubParts));
            CreateMap<UpdatePartRequest, UpdatePart>()
                .ForMember(dest => dest.SubParts, opts => opts.MapFrom(src => src.CreateSubParts));

            CreateMap<CreateProcedureRequest, CreateProcedure>().ReverseMap();
            CreateMap<UpdateProcedureRequest, UpdateProcedure>().ReverseMap();
            CreateMap<CreateProcedureStepRequest, CreateProcedureStep>();
            CreateMap<UpdateProcedureStepRequest, UpdateProcedureStep>();
            CreateMap<CreateProcedureStepMonitorRequest, CreateProcedureStepMonitor>();
            CreateMap<UpdateProcedureStepMonitorRequest, UpdateProcedureStepMonitor>();
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
            CreateMap<GetMultipleCustomersRequest, GetMultipleCustomers>();
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
            CreateMap<GetSensorValueRequest, GetSensorValue>();
            CreateMap<GetPurchasesRequest, GetPurchases>();
            CreateMap<CreatePurchaseRequest, CreatePurchase>();
            CreateMap<GetPurchaseOrderRequest, GetPurchaseOrder>();
            CreateMap<CreateQuoteRequest, CreateQuote>();
            CreateMap<CreateQuoteItemRequest, CreateQuoteItem>();
            CreateMap<GetQuoteRequest, GetQuote>();
            CreateMap<CreateProductRequest, CreateProduct>();
            CreateMap<GetProductRequest, GetProduct>();
            CreateMap<GetProductRequest, GetPurchaseOrderProduct>();
            CreateMap<UpdateProductRequest, UpdateProduct>();
            CreateMap<ProductStep, ProductStepModel>();
            CreateMap<CreateRoleRequest, CreateRole>();
            CreateMap<GetRoleUsersRequest, GetRolesUsers>();
            CreateMap<UpdateRoleRequest, UpdateRole>();
            CreateMap<Models.UserRoleModel, MSR.Domain.Models.UserRoleModel>();
            CreateMap<CreatePurchaseOrderRequest, CreatePurchaseOrder>();
            CreateMap<UpdatePurchaseOrderRequest, UpdatePurchaseOrder>();
            CreateMap<WorkOrderModel, WorkOrderGridSummary>()
                .ForMember(dest => dest.SerialNumber, opts => opts.MapFrom(src => (src.WorkOrderParts != null && src.WorkOrderParts.Count > 0) ? src.WorkOrderParts.First().SerialNumber : ""))
                .ForMember(dest => dest.PurchaseOrderNumber, opts => opts.MapFrom(src =>
                    src.Purchase != null ?
                        src.Purchase.PurchaseOrder != null ?
                            src.Purchase.PurchaseOrder.Id : 0
                        : 0))
                .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.Purchase != null ? src.Purchase.Qty : 0))
                .ForMember(dest => dest.CustomerLastRespondent, opts => opts.MapFrom(src => src.CustomerLastRespondent.GetValueOrDefault(false))); ;
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
            CreateMap<Models.MappedWorkOrderPart, MSR.Domain.Models.MappedWorkOrderPart>().ReverseMap();
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
            CreateMap<CreateWorkOrderMessageRequest, CreateWorkOrderMessage>()
                .ForMember(dest => dest.WorkOrderId, opts => opts.MapFrom(src => src.Id));
            CreateMap<GetPortalWorkOrderRequest, GetPortalWorkOrder>();

            CreateMap<GetArchiveDocumentRequest, GetArchiveDocument>();
            CreateMap<GetPartRequest, GetParts>();
            CreateMap<GetProcedureStepTemplateRequest, GetProcedureStepTemplate>();
            CreateMap<GetRolesRequest, GetRoles>();
            CreateMap<GetPortalWorkOrderRequest, GetPortalWorkOrderQueryModel>();
            CreateMap<Sort, QuerySort>();
            CreateMap<Filter, QueryFilter>();
            CreateMap<GetTrainingCertificationRequest, GetTrainingCertification>();
            CreateMap<GetWorkOrderHistoryRequest, GetWorkOrderHistory>();
            CreateMap<GetQuotesProductsRequest, GetQuotesProducts>();
            CreateMap<GetWorkflowStageRequest, GetWorkflowStageModel>();
            CreateMap<GetWorkflowGroupRequest, GetWorkflowGroupsModel>();
            CreateMap<GetPendingApprovalRequest, GetPendingApprovalModel>();
            CreateMap<GetProcedureTypeRequest, GetProcedureType>();
            CreateMap<GetProcedureRequest, GetProcedure>();
            CreateMap<GetWorkOrderMenuRequest, GetWorkOrderMenuQueryModel>();
            CreateMap<GetWorkflowRequest, GetWorkflowModel>();
            CreateMap<TakeOverWorkOrderRequest, TakeOverWorkOrder>();
            CreateMap<CancelWorkOrderRequest, CancelWorkOrder>();
            CreateMap<AddNCRWorkOrderTaskRequest, AddNCRWorkOrderTask>();
            CreateMap<GetPurchaseOrderDBRequest, GetPurchaseOrderDBView>();
            CreateMap<GetAssignedWorkOrdersRequest, GetAssignedWorkOrders>();
            CreateMap<GetQuotesProductsRequest, ProductDownloadFilter>();
            CreateMap<UpdateWorkOrderPriceRequest, UpdateWorkOrderPrice>()
                .ForMember(dest => dest.Price, opts => opts.Condition(src => src.Price > 0));
            CreateMap<CreatePurchase,PurchaseItem>();
        }
    }
}
