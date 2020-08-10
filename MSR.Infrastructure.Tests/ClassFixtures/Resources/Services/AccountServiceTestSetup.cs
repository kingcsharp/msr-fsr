using Microsoft.Extensions.DependencyInjection;
using MSR.Infrastructure.Resources.Services.Account;
using MSR.Infrastructure.Resources.Services.Workflow;
using Moq;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Abstractions.Services.Workflow;
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
using MockQueryable.Moq;

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

            var workflowGroupUserMap = new Mock<IRepository<WorkflowGroupUserMap>>();
            var workflowGroupUserMock = new List<WorkflowGroupUserMap>().AsQueryable().BuildMock();
            workflowGroupUserMap.Setup(m => m.Query()).Returns(workflowGroupUserMock.Object);
            mockUnitOfWork.SetupGet(m => m.WorkflowGroupUserMaps)
                .Returns(workflowGroupUserMap.Object);

            var workflowGroupStageMap = new Mock<IRepository<WorkflowGroupStageMap>>();
            var workflowGroupStageMapMock = new List<WorkflowGroupStageMap>().AsQueryable().BuildMock();
            workflowGroupStageMap.Setup(m => m.Query()).Returns(workflowGroupStageMapMock.Object);
            mockUnitOfWork.SetupGet(m => m.WorkflowGroupStageMaps)
                .Returns(workflowGroupStageMap.Object);

            var workflowActivityMap = new Mock<IRepository<WorkflowActivityMap>>();
            var workflowActivityMapMock = new List<WorkflowActivityMap>().AsQueryable().BuildMock();
            workflowActivityMap.Setup(m => m.Query()).Returns(workflowActivityMapMock.Object);
            mockUnitOfWork.SetupGet(m => m.WorkflowActivityMaps)
                .Returns(workflowActivityMap.Object);

            var workflowGroups = new Mock<IRepository<WorkflowGroup>>();
            var workflowGroupsMock = new List<WorkflowGroup>().AsQueryable().BuildMock();
            workflowGroups.Setup(m => m.Query()).Returns(workflowGroupsMock.Object);
            mockUnitOfWork.SetupGet(m => m.WorkflowGroups)
                .Returns(workflowGroups.Object);

            var workflowStages = new Mock<IRepository<WorkflowStage>>();
            var workflowStagesMock = new List<WorkflowStage>().AsQueryable().BuildMock();
            workflowStages.Setup(m => m.Query()).Returns(workflowStagesMock.Object);
            mockUnitOfWork.SetupGet(m => m.WorkflowStages)
                .Returns(workflowStages.Object);

            var workflowStagesMap = new Mock<IRepository<WorkflowStageMap>>();
            var workflowStagesMapMock = new List<WorkflowStageMap>().AsQueryable().BuildMock();
            workflowStagesMap.Setup(m => m.Query()).Returns(workflowStagesMapMock.Object);
            mockUnitOfWork.SetupGet(m => m.WorkflowStageMaps)
                .Returns(workflowStagesMap.Object);

            var workflowActivities = new Mock<IRepository<WorkflowActivity>>();
            var workflowActivitiesMock = new List<WorkflowActivity>().AsQueryable().BuildMock();
            workflowActivities.Setup(m => m.Query()).Returns(workflowActivitiesMock.Object);
            mockUnitOfWork.SetupGet(m => m.WorkflowActivities)
                .Returns(workflowActivities.Object);

            var menuRoles = new Mock<IRepository<MenuRole>>();
            var menuRolesMock = new List<MenuRole>().AsQueryable().BuildMock();
            menuRoles.Setup(m => m.Query()).Returns(menuRolesMock.Object);
            mockUnitOfWork.SetupGet(m => m.MenuRoles)
                .Returns(menuRoles.Object);

            Expression<Func<User, bool>> testExpression = i => i.UserName == Constants.GoodUserName;

            mockIRepositoryUser.Setup(m => m.FirstOrDefault(It.IsAny<bool>(),
                                        It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, object>>>()))
                .Returns(EFUserTestFixture.GoodUser);

            mockIRepositoryUser.Setup(m => m.FirstOrDefaultAsync(It.IsAny<bool>(),
                                        It.IsAny<Expression<Func<User, bool>>>(), null))
                .ReturnsAsync(EFUserTestFixture.GoodUser);

            var usersMock = new List<User>() { EFUserTestFixture.GoodUser }.AsQueryable().BuildMock();
            mockIRepositoryUser.Setup(m => m.Query()).Returns(usersMock.Object);

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
            .AddScoped<IWorkflowService, WorkflowService>()
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
