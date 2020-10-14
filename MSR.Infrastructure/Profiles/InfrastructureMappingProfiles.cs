using AutoMapper;
using MSR.Domain.Commands;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Linq;
using MSR.Domain.Models;
using Customer = MSR.Infrastructure.Resources.EntityFramework.Entities.Customer;
using HelpPage = MSR.Infrastructure.Resources.EntityFramework.Entities.HelpPage;
using MenuGroup = MSR.Infrastructure.Resources.EntityFramework.Entities.MenuGroup;
using MenuItem = MSR.Infrastructure.Resources.EntityFramework.Entities.MenuItem;
using Procedure = MSR.Infrastructure.Resources.EntityFramework.Entities.Procedure;
using ProcedureStepMonitor = MSR.Infrastructure.Resources.EntityFramework.Entities.ProcedureStepMonitor;
using ProcedureType = MSR.Infrastructure.Resources.EntityFramework.Entities.ProcedureType;
using Role = MSR.Infrastructure.Resources.EntityFramework.Entities.Role;
using TimeZone = MSR.Infrastructure.Resources.EntityFramework.Entities.TimeZone;
using User = MSR.Infrastructure.Resources.EntityFramework.Entities.User;
using System.Collections.Generic;
using Castle.Core.Internal;
using MSR.Domain.Validators;
using System.Runtime.InteropServices.ComTypes;

namespace MSR.Infrastructure.Profiles
{
    public class InfrastructureMappingProfiles : Profile
    {
        public InfrastructureMappingProfiles()
        {
            #region User
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Roles, opts => opts.Ignore())
                .ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.GetFullName()))
                .ReverseMap();
            CreateMap<CreateUser, User>();
            CreateMap<UpdateUser, User>();
            CreateMap<User, UserApproval>();
            #endregion

            #region Customer
            CreateMap<Customer, Domain.Models.Customer>()
                    .ForMember(dest => dest.Location, opts => opts.AllowNull())
                    .ForMember(dest => dest.PrimaryContactUser, opts => opts.AllowNull())
                    .ForMember(dest => dest.SecondaryContactUser, opts => opts.AllowNull())
                    .AfterMap((src, dest) => dest.Location = src.Location == null ? null : dest.Location)
                    .AfterMap((src, dest) => dest.PrimaryContactUser = src.PrimaryContactUser == null ? null : dest.PrimaryContactUser)
                    .AfterMap((src, dest) => dest.SecondaryContactUser = src.SecondaryContactUser == null ? null : dest.SecondaryContactUser);
            CreateMap<Domain.Models.Customer, Customer>();
            CreateMap<CustomerApproval, Domain.Models.Customer>()
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name));
            CreateMap<CreateCustomer, Customer>();
            CreateMap<CreateCustomer, CustomerApproval>();
            CreateMap<Customer, CustomerApproval>()
                .ForMember(dest => dest.SecondarContactUserId, opts => opts.MapFrom(src => src.SecondaryContactUserId))
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            CreateMap<UpdateCustomer, Customer>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateCustomer, CustomerApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.SecondarContactUserId, opts => opts.MapFrom(src => src.SecondaryContactUserId))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CustomerImportItem, CreateCustomer>();
            CreateMap<CustomerImportItem, UpdateCustomer>()
                .ForMember(dest => dest.CustomerId, opts => opts.MapFrom(src => src.Id));
            #endregion

            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<UpdateProduct, ProductApproval>().ReverseMap();
            CreateMap<CreateProduct, ProductApproval>().ReverseMap();
            CreateMap<Resources.EntityFramework.Entities.ProductStep, ProductStepModel>().ReverseMap();
            CreateMap<Purchase, PurchaseModel>();
            CreateMap<CreatePurchase, Purchase>().ReverseMap();
            CreateMap<WorkOrderPart, WorkOrderPartModel>().ReverseMap();
            CreateMap<WorkOrderTask, WorkOrderTaskModel>()
                .ForMember(dest => dest.TaskStarted, opts => opts.MapFrom(src => (
                    src.StartedOn != null && src.StartedOn.Value.Ticks > 0
                )));
            CreateMap<WorkOrderPart, WorkOrderPartModel>()
                .ForMember(dest => dest.Qty, opts => opts.MapFrom(src => WorkOrderPart_to_WorkOrderPartModel_qty(src)));
            CreateMap<WorkOrderPartModel, WorkOrderPart>();
            CreateMap<WorkOrderTask, WorkOrderTaskModel>().ReverseMap();
            CreateMap<WorkOrderTaskMonitor, WorkOrderTaskMonitorModel>().ReverseMap();
            CreateMap<UpdateWorkOrderTaskMonitor, WorkOrderTaskMonitor>();

            #region Location
            CreateMap<Location, LocationModel>().ReverseMap()
                .ForMember(dest => dest.TimeZone, opts => opts.Ignore());
            CreateMap<GetLocations, Location>();
            CreateMap<LocationModel, Location>().ReverseMap();
            CreateMap<LocationModel, LocationApproval>();
            CreateMap<LocationApproval, LocationModel>()
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.TimeZone, opts => opts.Ignore());
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
            CreateMap<Sensor, SensorModel>().ReverseMap();
            CreateMap<LocationImportItem, CreateLocation>();
            CreateMap<LocationImportItem, UpdateLocation>();
            #endregion

            CreateMap<TimeZone, TimeZoneModel>().ReverseMap();

            CreateMap<GetLocations, Location>();
            CreateMap<User, UserApproval>();

            CreateMap<GetLocations, Location>();
            CreateMap<Invoice, InvoiceModel>().ReverseMap();
            CreateMap<InvoiceItem, InvoiceItemModel>().ReverseMap();

            CreateMap<Role, Domain.Models.Role>()
                .ForMember(dest => dest.Menus, opt => opt.Ignore())
                .ForMember(dest => dest.ParentRoles, opt => opt.Ignore())
                .ForMember(dest => dest.IsCertificationRole, opts => opts.MapFrom(src => src.IsCertificationRole == null ? false : src.IsCertificationRole));


            #region Workflow
            CreateMap<CreateWorkflowGroupModel, WorkflowGroup>();
            CreateMap<UpdateWorkflowGroupModel, WorkflowGroup>();
            CreateMap<WorkflowGroupRoleMapModel, WorkflowGroupRoleMap>();
            CreateMap<WorkflowGroup, WorkflowGroupModel>();
            CreateMap<WorkflowGroupRoleMap, WorkflowGroupRoleMapModel>();
            #endregion


            #region Invoice
            CreateMap<Invoice, Domain.Models.InvoiceModel>().ReverseMap();
            CreateMap<InvoiceItem, Domain.Models.InvoiceItemModel>().ReverseMap();
            CreateMap<Invoice, InvoiceView>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.InvoiceDate))
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Created.GetFullName()))
                .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => GetLocationId(src)))
                .ForMember(dest => dest.LastUpdatedByName, opt => opt.MapFrom(src => src.LastUpdated.GetFullName()));
            CreateMap<InvoiceItem, InvoiceItemView>()
                .ForMember(dest => dest.PurchaseNumber, opt => opt.MapFrom(src => src.WorkOrder.Purchase.CustomerPurchaseNumber));
            CreateMap<CreateOneInvoice, Invoice>();
            CreateMap<CreateUpdateInvoiceItem, InvoiceItem>();
            CreateMap<UpdateInvoice, Invoice>();
            CreateMap<DownloadAsIIFInvoices, GetInvoices>();
            #endregion

            CreateMap<HelpPage, Domain.Models.HelpPage>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());


            CreateMap<UpdateMenuRoleMap, MenuRolePermission>()
                .ForMember(dest => dest.Created, opts => opts.Ignore())
                .ForMember(dest => dest.CreatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdated, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdatedOn, opts => opts.Ignore());

            #region Part
            CreateMap<Part, PartModel>()
                .ForMember(dest => dest.CreateSubParts, opt => opt.MapFrom(src => src.Subparts))
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Created.GetFullName()))
                .ForMember(dest => dest.LastUpdatedByName, opt => opt.MapFrom(src => src.LastUpdated.GetFullName()));


            CreateMap<PartSubPartMap, SubPartModel>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentPartId))
                .ForMember(dest => dest.PartId, opt => opt.MapFrom(src => src.PartId))
                .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty));


            CreateMap<CreatePart, PartApproval>();
            CreateMap<CreatePart, Part>().ForMember("Subparts", opts => opts.Ignore());
            CreateMap<SubPartModel, PartSubPartMap>();
            CreateMap<UpdatePart, PartApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.PartId, opts => opts.MapFrom(src => src.Id));
            CreateMap<UpdatePart, Part>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Domain.Models.PartModel, CreatePart>();
            CreateMap<Domain.Models.PartModel, UpdatePart>();
            #endregion

            #region Procedure
            CreateMap<Procedure, Domain.Models.Procedure>();
            CreateMap<ProcedureStep, ProcedureStepModel>()
                .ForMember(dest => dest.UtilizationTime, opts => opts.MapFrom(src => src.Utilization))
                .ForMember(dest => dest.ProcedureStepType, opts => opts.MapFrom(src => src.StepType.Name))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.ProcedureStepRoles))
                .ForMember(dest => dest.ProcedureStepTypeId, opts => opts.MapFrom(src => src.ProcedureStepTypeId.ToString()));
            CreateMap<ProcedureStepRoleMap, Domain.Models.Role>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Role.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Role.Name));
            CreateMap<ProcedureStep, WorkOrderTask>()
                .ForMember(dest => dest.ProcedureStepId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Created, opts => opts.Ignore())
                .ForMember(dest => dest.CreatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.CreatedOn, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdated, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdatedOn, opts => opts.Ignore())
                .ForMember(dest => dest.StatusId, opts => opts.MapFrom(src => 1))
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            CreateMap<ProcedureStepType, ProcedureStepTypeModel>().ReverseMap();

            // This mapping is correct according to the requirements
            // https://cmhworks.testlodge.com/projects/30813/requirements/32475
            // The fields are for procedure template, and commands for procedure
            // step template, and "procedure template" does not exist in the DB.
            // Likewise, in answer 2 there is no distinction.
            // TODO: This might need to be revisited.
            CreateMap<ProcedureStepTemplate, ProcedureStepTemplateModel>()
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.StepText))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => splitRoles(src)));

            CreateMap<ProcedureType, Domain.Models.ProcedureType>();
            CreateMap<WorkOrder, WorkOrderModel>();
            CreateMap<CreateWorkOrder, WorkOrder>();
            CreateMap<UpdateWorkOrder, WorkOrder>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(0)));
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
            CreateMap<CreateProcedureStepTemplate, ProcedureStepTemplate>()
                .ForMember(dest => dest.ReferenceFiles, opts => opts.Ignore())
                .ForMember(dest => dest.StepText, opts => opts.MapFrom(src => src.Text))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => String.Join(',', src.Roles)))
                .ForMember(dest => dest.StepText, opts => opts.MapFrom(src => src.Text));
            CreateMap<UpdateProcedureStepTemplate, ProcedureStepTemplate>()
                .ForMember(dest => dest.ReferenceFiles, opts => opts.Ignore())
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => String.Join(',', src.Roles)))
                .ForMember(dest => dest.StepText, opts => opts.MapFrom(src => src.Text))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(0)));
            CreateMap<CreateProcedureType, ProcedureType>();
            CreateMap<UpdateProcedureType, ProcedureType>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(0)));

            CreateMap<ProcedureStep, ProductStepModel>()
                .ForMember(dest => dest.LaborMinutes, opts => opts.MapFrom(src => src.LaborTime))
                .ForMember(dest => dest.EquipmentMinutes, opts => opts.MapFrom(src => src.EquipmentTime));

            CreateMap<Domain.Models.ProductStep, Resources.EntityFramework.Entities.ProductStep>();
            #endregion

            // Monitor
            CreateMap<Domain.Models.ProcedureStepMonitor, MonitorModel>();
            CreateMap<ProcedureStepMonitor, WorkOrderTaskMonitor>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.Created, opts => opts.Ignore())
                .ForMember(dest => dest.CreatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.CreatedOn, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdated, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.LastUpdatedOn, opts => opts.Ignore())
                .ForMember(dest => dest.ProcedureMonitorId, opts => opts.MapFrom(src => src.Id));
            CreateMap<ProcedureStepMonitor, Domain.Models.ProcedureStepMonitor>()
                .ForMember(dest => dest.InputType, opt => opt.MapFrom(src => src.InputType.Name))
                .ForMember(dest => dest.MonitorType, opt => opt.MapFrom(src => src.MonitorType.Name))
                .ForMember(dest => dest.TargetValue, opt => opt.MapFrom(src => src.Target.ToString()))
                .ForMember(dest => dest.FaultHandling, opt => opt.MapFrom(src => src.FailAction))
                .ForMember(dest => dest.SendEmailNotification, opt => opt.MapFrom(src => src.SendNCREmail));
            CreateMap<MonitorInputType, ProcedureStepMonitorInputType>()
                .ForMember(dest => dest.InputTypeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InputTypeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MonitorTypeName, opt => opt.MapFrom(src => src.Type.Name));
            CreateMap<MonitorListItem, ProcedureStepMonitorListItem>()
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ListItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ListName, opt => opt.MapFrom(src => src.List.Name));
            CreateMap<CreateProcedureStepMonitor, ProcedureStepMonitor>()
                .ForMember(dest => dest.MonitorType, opts => opts.Ignore()) // must be manually mapped
                .ForMember(dest => dest.InputType, opts => opts.Ignore());  // must be manually mapped
            CreateMap<UpdateProcedureStepMonitor, ProcedureStepMonitor>()
                .ForMember(dest => dest.MonitorType, opts => opts.Ignore()) // must be manually mapped
                .ForMember(dest => dest.InputType, opts => opts.Ignore())   // must be manually mapped
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
            //CreateMap<PurchaseOrderApproval, PurchaseOrder>().ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion

            CreateMap<MenuRolePermission, Permission>().ReverseMap();

            CreateMap<MenuItem, Domain.Models.MenuItem>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()).ReverseMap();
            CreateMap<MenuGroup, Domain.Models.MenuGroup>().ReverseMap();

            CreateMap<Status, StatusModel>().ReverseMap();

            #region Product
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<CreateProduct, Product>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            CreateMap<ProductModel, Product>();
            CreateMap<CreateProduct, ProductApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            CreateMap<Domain.Models.ProductStep, Resources.EntityFramework.Entities.ProductStep>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            CreateMap<UpdateProduct, ProductApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            CreateMap<Product, ProductApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.ProductId, opts => opts.MapFrom(i => i.Id));
            CreateMap<ProductModel, QuotesProductsView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.SubmittedById, opt => opt.MapFrom(src => src.CreatedBy.GetValueOrDefault()))
                .ForMember(dest => dest.SubmittedBy, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.ProcedureName, opt => opt.MapFrom(src => src.Procedure.Name))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PartKitNo, opt => opt.MapFrom(src => src.Part.Name));
            CreateMap<QuoteModel, QuotesProductsView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProcedureName, opt => opt.MapFrom(src => src.ProcessName))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Customer.Name));
            CreateMap<ProductApproval, ProductModel>()
                .ForMember(i => i.ApprovalStatus, opts => opts.MapFrom(src => src.Status.Name));
            #endregion

            #region Quote
            CreateMap<Quote, Domain.Models.QuoteModel>().ReverseMap();
            CreateMap<QuoteItem, Domain.Models.QuoteItemModel>().ReverseMap();
            CreateMap<CreateQuote, Quote>();
            CreateMap<CreateQuoteItem, QuoteItem>();
            #endregion


            CreateMap<UploadFile, FileModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.FileName));

            #region PurchaseOrder
            CreateMap<PurchaseOrderProduct, PurchaseOrderView>()
                .ForMember(dest => dest.CustomerId, opts => opts.MapFrom(src => src.PurchaseOrder.CustomerId))
                .ForMember(dest => dest.CustomerName, opts => opts.MapFrom(src => src.PurchaseOrder.Customer.Name))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.PurchaseOrder.Name))
                .ForMember(dest => dest.CustomerReferencePO, opts => opts.MapFrom(src => src.PurchaseOrder.CustomerReference))
                .ForMember(dest => dest.OpenDate, opts => opts.MapFrom(src => src.PurchaseOrder.OpenDate))
                .ForMember(dest => dest.CloseDate, opts => opts.MapFrom(src => src.PurchaseOrder.CloseDate));

            CreateMap<PurchaseOrder, PurchaseOrderModel>().ReverseMap();
            CreateMap<PurchaseOrder, PurchaseOrderView>()
                .ForMember(dest => dest.CustomerReferencePO, opts => opts.MapFrom(src => src.ReferencePO))
                .ForMember(dest => dest.CustomerReferenceNo, opts => opts.MapFrom(src => src.CustomerReference))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status == null ? "Open" : src.Status.Name));
            CreateMap<PurchaseOrderApproval, PurchaseOrderView>()
                .ForMember(dest => dest.CustomerReferencePO, opts => opts.MapFrom(src => src.ReferencePO))
                .ForMember(dest => dest.CustomerReferenceNo, opts => opts.MapFrom(src => src.CustomerReference))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status == null ? "Pending" : src.Status.Name));

            CreateMap<Product, PurchaseOrderProductView>();
            CreateMap<CreatePurchaseOrder, PurchaseOrder>()
                .ForMember(dest => dest.ReferencePO, opts => opts.MapFrom(src => src.CustomerReferencePO))
                .ForMember(dest => dest.CustomerReference, opts => opts.MapFrom(src => src.CustomerReferenceNo));
            CreateMap<CreatePurchaseOrder, PurchaseOrderApproval>()
                .ForMember(dest => dest.ReferencePO, opts => opts.MapFrom(src => src.CustomerReferencePO))
                .ForMember(dest => dest.CustomerReference, opts => opts.MapFrom(src => src.CustomerReferenceNo));
            CreateMap<PurchaseOrderApproval, PurchaseOrder>();
            CreateMap<UpdatePurchaseOrder, PurchaseOrder>()
                .ForMember(dest => dest.ReferencePO, opts => opts.MapFrom(src => src.CustomerReferencePO))
                .ForMember(dest => dest.CustomerReference, opts => opts.MapFrom(src => src.CustomerReferenceNo))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdatePurchaseOrder, PurchaseOrderApproval>()
                .ForMember(dest => dest.ReferencePO, opts => opts.MapFrom(src => src.CustomerReferencePO))
                .ForMember(dest => dest.CustomerReference, opts => opts.MapFrom(src => src.CustomerReferenceNo))
                .ForMember(dest => dest.PurchaseOrderId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            CreateMap<PurchaseOrder, PurchaseOrderApproval>()
                .ForMember(dest => dest.PurchaseOrderId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            #endregion

            CreateMap<File, FileModel>()
                .ForMember(dest => dest.FileId, opts => opts.MapFrom(src => src.Id));
            CreateMap<FileEntityMap, FileModel>()
                .ForMember(dest => dest.FileId, opts => opts.MapFrom(src => src.FileObject.Id))
                .ForMember(dest => dest.ContentType, opts => opts.MapFrom(src => src.FileObject.ContentType))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.FileObject.Name))
                .ForMember(dest => dest.FileURL, opts => opts.MapFrom(src => src.FileObject.FileURL));

            CreateMap<AdminCostSetting, AdminCostSettingsModel>().ReverseMap();

            CreateMap<PartCSVRecord, PartModel>();
            CreateMap<PartCSVRecord, UpdatePart>();
            CreateMap<PartCSVRecord, CreatePart>();

            #region Reporting
            CreateMap<Report, ReportModel>();
            CreateMap<ReportCategory, ReportCategoryModel>();
            CreateMap<ReportDashboard, ReportDashboardModel>()
                .ForMember(dest => dest.Reports, opts => opts.Ignore());
            #endregion
            CreateMap<PurchaseOrderProduct, PurchaseOrderProductView>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductId, opts => opts.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.TotalSalePrice, opts => opts.MapFrom(src => src.Product.TotalSalePrice))
                .ForMember(dest => dest.PartName, opts => opts.MapFrom(src => src.Product.Part.Name))
                .ForMember(dest => dest.PartNumber, opts => opts.MapFrom(src => src.Product.Part.PartNumber))
                .ForMember(dest => dest.ProcedureName, opts => opts.MapFrom(src => src.Product.Procedure.Name));

            CreateMap<PurchaseModel, CreateWorkOrder>()
                .ForMember(dest => dest.Qty, opts => opts.MapFrom(src => src.Qty > 0 ? src.Qty : 1))
                .ForMember(dest => dest.PurchaseId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, opts => opts.MapFrom(src => src.PurchaseOrderProduct.ProductId))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.PurchasePrice))
                .ForMember(dest => dest.ScheduledEndDate, opts => opts.MapFrom(src => src.DueDate));

            CreateMap<UpdateWorkOrderPart, WorkOrderPart>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.WorkOrderPartId));

            CreateMap<CreateWorkOrderTask, WorkOrderTask>();
            CreateMap<UpdateWorkOrderTask, WorkOrderTask>()
                .ForMember(dest => dest.Status, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => ignoreNullOrZero(srcMember)));

            CreateMap<EquipmentMaintenance, EquipmentMaintenanceModel>().ReverseMap();
            CreateMap<CreateEquipmentMaintenance, EquipmentMaintenance>();
            CreateMap<Document, DocumentView>();
            CreateMap<CreateDocument, Document>();
            CreateMap<CreateDocument, DocumentApproval>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
            CreateMap<DocumentApproval, DocumentView>();
            CreateMap<DocumentApproval, DocumentView>();
            CreateMap<UpdateDocument, Document>();
            CreateMap<UpdateDocument, DocumentApproval>();
            CreateMap<DocumentRoleMap, RoleView>();
        }

        private static bool ignoreNullOrZero(object srcMember)
        {
            if (srcMember == null) {
                return false;
            }

            if ((srcMember is int) && (int)srcMember == 0) {
                return false;
            }

            if ((srcMember is DateTime) && ((DateTime)srcMember).Ticks == 0) {
                return false;
            }

            return true;
        }

        private static List<int> splitRoles(ProcedureStepTemplate arg)
        {
            List<int> ret;

            try
            {
                if (arg.Roles.IsNullOrEmpty())
                {
                    ret = new List<int>();
                }
                else
                {
                    ret = arg.Roles.Split(',')
                        .Select(x => Convert.ToInt32(x))
                        .ToList();
                }
            }
            catch (FormatException)
            {
                // ignore bad data
                ret = new List<int>();
            }
            return ret;
        }

        private int? GetLocationId(Invoice src)
        {
            return src.InvoiceItems?.FirstOrDefault()?.WorkOrder?.Purchase?.LocationId;
        }

        private int? WorkOrderPart_to_WorkOrderPartModel_qty(WorkOrderPart src)
        {
            if (src.WorkOrder != null && src.WorkOrder.Purchase != null)
            {
                return src.WorkOrder.Purchase.Qty;
            }
            return null;
        }
    }
}
