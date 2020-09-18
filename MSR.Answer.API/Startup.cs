using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSR.Answer.API.Extentions;
using MSR.Application.Hubs;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;
using System.Net;

namespace MSR.Answer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();
            configuration = builder.Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();
            services.AddApiServices(Configuration);
            services.AddOpenApiDocument(settings =>
            {
                settings.DocumentName = "Answer3";
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "MSR API";
                    document.Info.Description = "REST API Answer 3";
                };

                settings.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
                settings.AddSecurity("Bearer", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = nameof(Authorization),
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Copy this into the value field: Bearer {token}"
                    }
                );
            });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/msg");
            });
        }
    }
}
