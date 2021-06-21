using System;
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
using MSR.Infrastructure.Resources.Answer;
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
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config, GeneralInformation generalInfo)
        {
            var dbConfig = config.GetSection(nameof(DatabaseInformation)).Get<DatabaseInformation>();

            services.AddDbContext<AnswerContext>(optionsBuilder => optionsBuilder.UseSqlServer(dbConfig.ConnectionString));
            services.AddHttpClient<IAnswerRestClient,AnswerClient>(c => c.BaseAddress = new Uri(generalInfo.APIURL));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPartService, PartService>();
            services.AddScoped<IProcedureService, ProcedureService>();
            services.AddScoped<IProcedureStepMonitorService, ProcedureStepMonitorService>();
            services.AddScoped<IProcedureStepTemplateService, ProcedureStepTemplateService>();
            services.AddScoped<IProcedureTypeService, ProcedureTypeService>();
            services.AddScoped<IWorkOrderService, WorkOrderService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IWorkflowStageService, WorkflowStageService>();
            services.AddScoped<IWorkflowApprovalService, WorkflowApprovalService>();
            services.AddScoped<IWorkflowGroupService, WorkflowGroupService>();
            services.AddScoped<IWorkflowService, WorkflowService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IHelpService, HelpService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IQuickbooksService, QuickbooksService>();
            services.AddSingleton<IFileHandlerFactory, FileHandlerFactory>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IImportValidatorFactory, ImportValidatorFactory>();
            services.AddTransient<S3FileHandler>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<ITimezoneService, TimezoneService>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddScoped<IAdminCostSettingsService, AdminCostSettingsService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IEquipmentMaintenanceService, EquipmentMaintenanceService>();
            services.AddScoped<IDocumentService, DocumentService>();
            
            services.AddSingleton<IMessageHubClient, MessageHubService>();

            return services;
        }
    }
}
