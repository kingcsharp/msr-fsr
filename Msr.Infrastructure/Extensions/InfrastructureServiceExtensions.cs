using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Abstractions.Email;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Abstractions.Services.Workflow;
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
using Amazon.S3;
using MSR.Domain.Abstractions;
using MSR.Infrastructure.Factories;
using MSR.Infrastructure.Resources.AWS;
using Amazon.Runtime;

namespace MSR.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            var dbConfig = config.GetSection(nameof(DatabaseInformation)).Get<DatabaseInformation>();
            services.AddDbContext<AnswerContext>(optionsBuilder => optionsBuilder.UseLazyLoadingProxies().UseSqlServer(dbConfig.ConnectionString));
            var s3Config = config.GetSection(nameof(S3Information)).Get<S3Information>();
            services.AddSingleton(s3Config);
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IWorkflowStageService, WorkflowStageService>();
            services.AddScoped<IWorkflowApprovalService, WorkflowApprovalService>();
            services.AddScoped<IWorkflowGroupService, WorkflowGroupService>();
            services.AddScoped<IWorkflowService, WorkflowService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IHelpService, HelpService>();
            services.AddScoped<IAmazonS3>(i => new AmazonS3Client(new BasicAWSCredentials(s3Config.AWSAccessKey, s3Config.AWSSecretKey),Amazon.RegionEndpoint.USEast1));
            services.AddSingleton<IFileHandlerFactory, FileHandlerFactory>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<S3FileHandler>();

            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            return services;
        }
    }
}
