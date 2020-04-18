using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Resources.Services.Account.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Answer.API.Extentions
{
    public static class JWTServiceExtentions
    {
        public static IServiceCollection AddJWTServices(this IServiceCollection services, IConfiguration config)
        {
            var jwtData = config.GetSection(nameof(JwtData)).Get<JwtData>();
            services.AddSingleton(jwtData);
            var key = Encoding.ASCII.GetBytes(jwtData.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var accountService = context.HttpContext.RequestServices.GetRequiredService<IAccountService>();
                        var accountId = int.Parse(context.Principal.Identity.Name);
                        var user = accountService.ValidateAccount(accountId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
