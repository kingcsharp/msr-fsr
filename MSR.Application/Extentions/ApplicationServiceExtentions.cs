using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Commanding.Abstractions;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using System.IO;
using MSR.Domain.Helpers;
using MSR.Application.ApplicationServices;
using MSR.Application.EventServices;
using MSR.Domain.Abstractions.Services;
using MSR.Application.Abstractions;
using MSR.Application.ViewServices;

namespace MSR.Application.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<AccountAppService>();
            services.AddTransient<CustomerAppService>();
            services.AddTransient<HelpAppService>();
            services.AddTransient<LocationAppService>();
            services.AddTransient<MenuAppService>();
            services.AddTransient<PartAppService>();
            services.AddTransient<ProcedureAppService>();
            services.AddTransient<ProcedureStepMonitorAppService>();
            services.AddTransient<ProcedureStepTemplateAppService>();
            services.AddTransient<ProcedureTypeAppService>();
            services.AddTransient<WorkOrderAppService>();
            services.AddTransient<RoleAppService>();
            services.AddTransient<UserAppService>();
            services.AddTransient<WorkflowAppService>();
            services.AddTransient<EventServiceHandler>();
            services.AddTransient<TimezoneAppService>();
            services.AddTransient<ReportAppService>();
            services.AddTransient<SearchAppService>();
            services.AddTransient<IWorkOrderViewService, WorkOrderViewService>();
            services.AddTransient<IProductViewService, ProductViewService>();
            services.AddTransient<IWorkOrderPartViewService, WorkOrderPartViewService>();

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
