using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSR.Application.Extentions;
using MSR.Domain.Extensions;
using MSR.Domain.Helpers;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Extensions;
using Serilog;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
            services.AddSingleton(mapperConfiguration.CreateMapper());
            AutoMapperHelper.Initialize(mapperConfiguration);
            services.AddApplicationServices();
            services.AddDomainServices(configuration);
            services.AddInfrastructureServices(configuration);
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowAnyHeader();
            }));

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
