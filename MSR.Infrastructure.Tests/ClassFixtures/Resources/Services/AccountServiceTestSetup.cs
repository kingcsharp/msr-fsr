using Microsoft.Extensions.DependencyInjection;
using MSR.Infrastructure.Resources.Services.Account;
using Moq;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;

namespace MSR.Infrastructure.Tests.ClassFixtures.Resources.Services
{
    public class AccountServiceTestSetup
    {
        public ServiceProvider ServiceProvider { get; }

        public AccountServiceTestSetup()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockIRepositoryUser = new Mock<IRepository<User>>();

            //mockIRepositoryUser.Setup(m => m.FirstOrDefault(It.IsAny<bool>(),It.Is<Expression<Func<User,bool>>>(criteria => LambdaCompare.Eq))

            mockUnitOfWork.SetupGet(m => m.Users).Returns(mockIRepositoryUser.Object);
            

            var services = new ServiceCollection()
            .AddLogging()
            .AddScoped<IAccountService, AccountService>();


            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
