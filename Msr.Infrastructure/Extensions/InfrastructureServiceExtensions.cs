using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Abstractions.Email;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Resources.Email;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.Services.Account;
using MSR.Infrastructure.Resources.Services.Location;
using MSR.Infrastructure.Resources.Services.Menu;
using MSR.Infrastructure.Resources.Services.Users;
using MSR.Infrastructure.Resources.Services.Workflow;

namespace MSR.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IWorkflowService, WorkflowService>();

            return services;
        }
    }
}
