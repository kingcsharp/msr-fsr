using Amazon.CloudWatchLogs;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSR.Application.Extentions;
using MSR.Domain.Extensions;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Extensions;
using NSwag;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.AwsCloudWatch;

namespace MSR.Answer.API.Extentions
{
    public static class ApiServiceExtentions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
        {
            var emailConfig = config.GetSection(nameof(EmailInformation)).Get<EmailInformation>();
            var generalConfig = config.GetSection(nameof(GeneralInformation)).Get<GeneralInformation>();
            services.AddSingleton(generalConfig);
            services.AddSingleton(emailConfig);

            var mapperConfiguration = new MapperConfiguration(i =>
            {
                i.AddMaps(new[]
                {
                    "MSR.Answer.Api",
                    "MSR.Application",
                    "MSR.Domain",
                    "MSR.Infrastructure"
                });
            });

            services.AddSingleton(mapperConfiguration.CreateMapper());

            services.AddApplicationServices();
            services.AddDomainServices();
            services.AddInfrastructureServices(config);
            services.AddJWTServices(config);
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowAnyHeader();
            }));

            return services;
        }
    }
}
