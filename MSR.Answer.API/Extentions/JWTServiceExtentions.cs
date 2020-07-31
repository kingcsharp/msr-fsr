using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models.Config;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MSR.Domain.Helpers;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using MSR.Domain.Commanding.Enums;
using System;

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
                        if (!int.TryParse(context.Principal.FindFirst(ClaimTypes.Name)?.Value, out var accountId)) context.Fail("Unauthorized");
                        if (!accountService.ValidateAccount(accountId))
                        {// return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        string claimVal = context.Principal.FindFirst(c => c.Type == "ApprovalPrivileges").Value;
                        var approvalPrivilegesDic = JsonConvert.DeserializeObject<Dictionary<int, int[]>>(claimVal);

                        string userPrivileges = context.Principal.FindFirst(c => c.Type == "Privileges").Value;
                        var deserializedUserPrivileges = JsonConvert.DeserializeObject<int[][]>(userPrivileges);

                        var curUser = new CurrentUserInformation(accountId, approvalPrivilegesDic, deserializedUserPrivileges);

                        services.AddSingleton(curUser);

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
