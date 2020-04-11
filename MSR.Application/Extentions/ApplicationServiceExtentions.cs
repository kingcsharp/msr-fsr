using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using System.IO;
using MSR.Domain.Helpers;

namespace MSR.Application.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<SystemLogin>, MsrAppService>();
            services.AddScoped<MsrAppService>();
            var assemblies = new List<Assembly>();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(path, "MSR.*.dll"))
            {
                assemblies.Add(Assembly.LoadFile(dll));
            }

            List<TypeInfo> typeList = assemblies.SelectMany(a => a.DefinedTypes).Where(t => t.IsClass && SystemTypeExtensions.ImplementsInterfaceOf<ICommandHandler>(t)).ToList();

            foreach (Type type in typeList)
            {
                foreach (Type serviceType in ((IEnumerable<Type>)type.GetInterfaces()).Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)).ToList())
                {
                    services.AddTransient(serviceType, type);
                }
            }
            return services;
        }
    }
}
