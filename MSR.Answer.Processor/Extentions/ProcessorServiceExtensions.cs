using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSR.Answer.Processor.SQSServices;
using MSR.Answer.Processor.SQSServices.Abstractions;
using MSR.Application.Extentions;
using MSR.Domain.Commanding;
using MSR.Domain.Extensions;
using MSR.Domain.Helpers;
using MSR.Domain.Models.Config;
using MSR.Domain.SQSEventing;
using MSR.Domain.SQSEventing.Abstractions;
using MSR.Infrastructure.Extensions;
using Serilog;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MSR.Answer.Processor.Extentions
{
    public static class ProcessorServiceExtensions
    {
        public static IServiceCollection AddProcessorServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration.GetSection(nameof(EmailInformation)).Get<EmailInformation>();
            var generalConfig = configuration.GetSection(nameof(GeneralInformation)).Get<GeneralInformation>();
            services.AddSingleton(generalConfig);
            services.AddSingleton(emailConfig);

            var mapperConfiguration = new MapperConfiguration(i =>
            {
                i.AddMaps(new[]
                {
                    "MSR.Answer.Processor",
                    "MSR.Application",
                    "MSR.Domain",
                    "MSR.Infrastructure"
                });
                i.AllowNullCollections = true;
                i.AllowNullDestinationValues = true;
            });


            //The CreateMapper will create the DI Mapper.  AutoMapperHelper Gives us a Static Mapper.  We need this 
            //When using AutoMapper in Static or other places where using Instance isn't required or feesable like the 
            //Api Request Class => Domain Command extention methods.  
            AutoMapperHelper.Initialize(mapperConfiguration);
            services.AddApplicationServices();
            services.AddDomainServices(configuration);
            services.AddInfrastructureServices(configuration);
            //services.AddJWTServices(configuration);
            services.AddSingleton(mapperConfiguration.CreateMapper());

            var assemblies = new List<Assembly>();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(path, "MSR.*.dll"))
            {
                assemblies.Add(Assembly.LoadFile(dll));
            }

            var eventHandlers = new EventHandlers();
            List<TypeInfo> eventTypeList = assemblies.SelectMany(a => a.DefinedTypes).Where(t => t.IsClass && SystemTypeExtensions.ImplementsInterfaceOf<IEventHandler>(t)).ToList();

            foreach (Type type in eventTypeList)
            {
                foreach (Type serviceType in ((IEnumerable<Type>)type.GetInterfaces()).Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>)).ToList())
                {
                    services.AddTransient(serviceType, type);
                    var args = serviceType.GetGenericArguments();
                    foreach (var argType in args)
                    {
                        services.AddScoped(typeof(EventDispatcher<>).MakeGenericType(argType));
                        eventHandlers.AddReference(argType);
                    }
                }
            }
            services.AddSingleton<IEventHandlers>(eventHandlers);
            services.AddSingleton<ISqsConsumerService, SqsConsumerService>();

            services.AddLogging();
            var loggerConfig = new LoggerConfiguration()
                .WriteTo.Console(new JsonFormatter())
                .WriteTo.Rollbar("0e34b5fc000342528dc361a4bb90f085", environment: generalConfig.Environment, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning);

            Log.Logger = loggerConfig.CreateLogger();
            services.AddLogging(loggerConfig => loggerConfig.AddSerilog(dispose: true));

            return services;
        }
    }
}
