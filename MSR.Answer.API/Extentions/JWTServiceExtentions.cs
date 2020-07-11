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
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Collections.Generic;
using MSR.Domain.Commanding.Enums;

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
                        var user = accountService.ValidateAccount(accountId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }

                        DelegateHandler.GetCurrentUserId = () => accountId;

                        string claimVal = context.Principal.FindFirst(c => c.Type == "ApprovalPrivileges").Value;

                        var approvalPrivilegesDic = JsonConvert.DeserializeObject<Dictionary<int, int[]>>(claimVal);

                        DelegateHandler.CanApproveActivity = (EnumApprovalTables) =>
                        {
                            var activityToBeApproved = (int)EnumApprovalTables;

                            approvalPrivilegesDic.TryGetValue(activityToBeApproved, out int[] privileges);

                            return privileges == null ? false : privileges.Contains((int)EnumPrivilege.CanApprove);
                        };

                        DelegateHandler.CanReadActivity = (EnumApprovalTables) =>
                        {
                            var activityToBeApproved = (int)EnumApprovalTables;

                            approvalPrivilegesDic.TryGetValue(activityToBeApproved, out int[] privileges);

                            return privileges == null ? false : privileges.Contains((int)EnumPrivilege.CanRead);
                        };

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
