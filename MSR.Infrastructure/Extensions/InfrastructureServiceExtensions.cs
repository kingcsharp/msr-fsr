using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Abstractions.Email;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Abstractions.QuickBooks;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Helpers;
using MSR.Infrastructure.Helpers.Abstractions;
using MSR.Infrastructure.Resources.Email;
using MSR.Infrastructure.Resources.EntityFramework;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.Services;
using MSR.Infrastructure.Resources.Services.Account;
using MSR.Infrastructure.Resources.Services.Location;
using MSR.Infrastructure.Resources.Services.Customers;
using MSR.Infrastructure.Resources.Services.Menu;
using MSR.Infrastructure.Resources.Services.Role;
using MSR.Infrastructure.Resources.Services.Users;
using MSR.Infrastructure.Resources.Services.Workflow;
using MSR.Infrastructure.Resources.Services.Help;
using MSR.Infrastructure.Resources.Services.Invoices;
using MSR.Infrastructure.Resources.Services.MessageHub;
using MSR.Domain.Abstractions;
using MSR.Infrastructure.Factories;
using MSR.Infrastructure.Resources.AWS;
using MSR.Infrastructure.Resources.Services.Sensor;
using MSR.Infrastructure.Resources.Services.PurchaseOrder;
using MSR.Infrastructure.Resources.Services.Timezone;
using MSR.Infrastructure.Resources.Services.Part;
using MSR.Infrastructure.Resources.Services.WorkOrder;
using MSR.Infrastructure.Resources.Services.AdminCostSetting;
using MSR.Infrastructure.Resources.Services.Report;
using MSR.Infrastructure.Resources.Services.Search;
using MSR.Infrastructure.Resources.Services.Document;
using MSR.Infrastructure.Resources.Services.EquipmentMaintenance;

namespace MSR.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            var dbConfig = config.GetSection(nameof(DatabaseInformation)).Get<DatabaseInformation>();

            services.AddDbContext<AnswerContext>(optionsBuilder => optionsBuilder.UseSqlServer(dbConfig.ConnectionString), contextLifetime:ServiceLifetime.Transient,optionsLifetime:ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IPartService, PartService>();
            services.AddTransient<IProcedureService, ProcedureService>();
            services.AddTransient<IProcedureStepMonitorService, ProcedureStepMonitorService>();
            services.AddTransient<IProcedureStepTemplateService, ProcedureStepTemplateService>();
            services.AddTransient<IProcedureTypeService, ProcedureTypeService>();
            services.AddTransient<IWorkOrderService, WorkOrderService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IWorkflowStageService, WorkflowStageService>();
            services.AddTransient<IWorkflowApprovalService, WorkflowApprovalService>();
            services.AddTransient<IWorkflowGroupService, WorkflowGroupService>();
            services.AddTransient<IWorkflowService, WorkflowService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IHelpService, HelpService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IQuickbooksService, QuickbooksService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ISensorService, SensorService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IQuoteService, QuoteService>();
            services.AddTransient<ITimezoneService, TimezoneService>();
            services.AddTransient<IAuthenticationHelper, AuthenticationHelper>();
            services.AddTransient<IAdminCostSettingsService, AdminCostSettingsService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IEquipmentMaintenanceService, EquipmentMaintenanceService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<S3FileHandler>();
            services.AddTransient<IMessageHubClient, MessageHubService>();

            services.AddSingleton<IFileHandlerFactory, FileHandlerFactory>();
            services.AddSingleton<IImportValidatorFactory, ImportValidatorFactory>();


            return services;
        }
    }
}
