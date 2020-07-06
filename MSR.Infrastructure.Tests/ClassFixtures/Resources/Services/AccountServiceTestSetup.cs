using Microsoft.Extensions.DependencyInjection;
using MSR.Infrastructure.Resources.Services.Account;
using Moq;
using MSR.Domain.Abstractions.Services;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using System.Linq.Expressions;
using System;
using MSR.Infrastructure.Tests.TestFixtures;
using AutoMapper;
using MSR.Infrastructure.Profiles;
using MSR.Domain.Abstractions.Email;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using MSR.Domain.Models.Config;
using System.Linq;
using MSR.Infrastructure.Helpers.Abstractions;

namespace MSR.Infrastructure.Tests.ClassFixtures.Resources.Services
{
    public class AccountServiceTestSetup
    {
        public ServiceProvider ServiceProvider { get; }

        public AccountServiceTestSetup()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockIRepositoryUser = new Mock<IRepository<User>>();
            var mockEmailService = new Mock<IEmailService>();
            var mockAuthenticationHelper = new Mock<IAuthenticationHelper>();

            Expression<Func<User, bool>> testExpression = i => i.UserName == Constants.GoodUserName;

            mockIRepositoryUser.Setup(m => m.FirstOrDefault(It.IsAny<bool>(),
                                        It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, object>>>()))
                .Returns(EFUserTestFixture.GoodUser);

            mockIRepositoryUser.Setup(m => m.FirstOrDefaultAsync(It.IsAny<bool>(),
                                        It.IsAny<Expression<Func<User, bool>>>(), null))
                .ReturnsAsync(EFUserTestFixture.GoodUser);
            
            mockIRepositoryUser.Setup(m => m.Query())
                .Returns(new List<User>() { EFUserTestFixture.GoodUser }.AsQueryable());
                       
            mockUnitOfWork.SetupGet(m => m.Users)
                .Returns(mockIRepositoryUser.Object);

            mockEmailService.Setup(i => i.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<bool>(), It.IsAny<List<Attachment>>()))
                .Returns(Task.FromResult(true));

            mockAuthenticationHelper.Setup(i => i.VerifyPasswordHash(It.Is<string>(i => i == Constants.GoodPassword), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .Returns(true);
            mockAuthenticationHelper.Setup(i => i.VerifyPasswordHash(It.Is<string>(i => i == Constants.FailPassword), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .Returns(false);
            mockAuthenticationHelper.Setup(i => i.VerifyPasswordHash(It.Is<string>(i => i == Constants.ExceptionPassword), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .Throws<ArgumentException>();


            var services = new ServiceCollection()
            .AddLogging()
            .AddScoped<IAccountService, AccountService>()
            .AddAutoMapper(typeof(InfrastructureMappingProfiles))
            .AddSingleton(mockIRepositoryUser.Object)
            .AddSingleton(mockUnitOfWork.Object)
            .AddSingleton(mockEmailService.Object)
            .AddSingleton(mockAuthenticationHelper.Object)
            .AddSingleton(new JwtData() { Secret = "ks1YvoEvXrofzYLABCDE9hNqM9gafA0cciHg7S31nwATocYvhjj2VF5xTAd5z1NQQid6zqAEUcaDBpuXeRP8A3hbURV10NEESXxSlPWjLINn4U6DEAysjPWeexsXPI" })
            .AddSingleton(new EmailInformation() 
            { 
                EnableSsl = false, 
                From = "test@test.com", 
                Host = "test.com", 
                Password = "test", 
                PasswordReminderEmailsTo = "test@test.com", 
                Port = 443, SendEmailsTo = "test@test.com", 
                SupportEmail = "test@test.com", 
                UserName = "Test" 
            })
            .AddSingleton(new GeneralInformation()
            {
                Environment = "test",
                RollbarConfig = "test",
                WebsiteURL = "test.com"
            });
            
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
