using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.Services.Account;
using MSR.Infrastructure.Resources.Services.Account.Abstractions;

namespace MSR.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(config.GetSection(nameof(JwtData)).Get<JwtData>());

            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
