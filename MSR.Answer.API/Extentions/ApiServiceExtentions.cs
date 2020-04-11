using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MSR.Application.Extentions;
using MSR.Domain.Extensions;

namespace MSR.Answer.API.Extentions
{
    public static class ApiServiceExtentions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
        {
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
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen(i =>
            {
                i.SwaggerDoc("v1", new OpenApiInfo { Title = "MSR API", Version = "v1" });
            });

            var jwtData = config.GetSection("JWT");
            services.Configure<JwtData>(jwtData)
            return services;
        }
    }
}
