using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;

namespace MSR.Domain.Extensions
{
    public static class DomainServiceExtentions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            return services;
        }
    }
}
