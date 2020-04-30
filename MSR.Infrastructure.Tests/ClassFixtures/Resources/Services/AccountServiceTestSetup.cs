using Microsoft.Extensions.DependencyInjection;
using MSR.Infrastructure.Resources.Services.Account;
using Moq;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using System.Linq.Expressions;
using System;
using Neleus.LambdaCompare;
using MSR.Infrastructure.Tests.TestFixtures;
using MSR.Domain.Abstractions.Services;

namespace MSR.Infrastructure.Tests.ClassFixtures.Resources.Services
{
    public class AccountServiceTestSetup
    {
        public ServiceProvider ServiceProvider { get; }

        public AccountServiceTestSetup()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockIRepositoryUser = new Mock<IRepository<User>>();
            Expression<Func<User, bool>> testExpression = user => user.UserName == Constants.GoodUserName;

            mockIRepositoryUser.Setup(m => m.FirstOrDefault(It.IsAny<bool>(), 
                                        It.Is<Expression<Func<User, bool>>>(criteria => Lambda.Eq(criteria, testExpression))))
                               .Returns(EFUserTestFixture.GoodUser);
            
           
            mockUnitOfWork.SetupGet(m => m.Users).Returns(mockIRepositoryUser.Object);
            

            var services = new ServiceCollection()
            .AddLogging()
            .AddScoped<IAccountService, AccountService>();


            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
