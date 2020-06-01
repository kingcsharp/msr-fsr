using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Abstractions.Email;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Helpers;
using MSR.Infrastructure.Helpers.Abstractions;
using MSR.Infrastructure.Resources.Email;
using MSR.Infrastructure.Resources.EntityFramework;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.Services.Account;
using MSR.Infrastructure.Resources.Services.Menu;
using MSR.Infrastructure.Resources.Services.Users;
using MSR.Infrastructure.Resources.Services.Workflow;

namespace MSR.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            var dbConfig = config.GetSection(nameof(DatabaseInformation)).Get<DatabaseInformation>();
            services.AddDbContext<AnswerContext>(optionsBuilder => optionsBuilder.UseLazyLoadingProxies().UseSqlServer(dbConfig.ConnectionString));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IWorkflowService, WorkflowService>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();


            return services;
        }
    }
}
