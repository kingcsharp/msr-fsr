using Microsoft.Extensions.DependencyInjection;
using MSR.Infrastructure.Resources.Services.Account;
using Moq;
using MSR.Domain.Abstractions.Services;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using System.Linq.Expressions;
using System;
using Neleus.LambdaCompare;
using MSR.Infrastructure.Tests.TestFixtures;
using AutoMapper;
using MSR.Infrastructure.Profiles;
using MSR.Domain.Abstractions.Email;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using MSR.Domain.Models.Config;

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

            Expression<Func<User, bool>> testExpression = user => user.UserName == Constants.GoodUserName;

            mockIRepositoryUser.Setup(m => m.FirstOrDefault(It.IsAny<bool>(), 
                                        It.Is<Expression<Func<User, bool>>>(criteria => Lambda.Eq(criteria, testExpression))))
                               .Returns(EFUserTestFixture.GoodUser);
           
            mockUnitOfWork.SetupGet(m => m.Users).Returns(mockIRepositoryUser.Object);

            mockEmailService.Setup(i => i.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<bool>(), It.IsAny<Attachment>()))
                .Returns(Task.FromResult(true));

            var services = new ServiceCollection()
            .AddLogging()
            .AddScoped<IAccountService, AccountService>()
            .AddAutoMapper(typeof(InfrastructureMappingProfiles))
            .AddSingleton(mockIRepositoryUser.Object)
            .AddSingleton(mockUnitOfWork.Object)
            .AddSingleton(mockEmailService.Object)
            .AddSingleton(new JwtData() { Secret = "ABC123" })
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
