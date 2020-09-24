using Microsoft.Extensions.DependencyInjection;
using Moq;
using MSR.Infrastructure.Resources.EntityFramework.Application;

namespace MSR.Infrastructure.Tests.ClassFixtures.Resources.Services
{
    public class AccountServiceTestSetup
    {
        public ServiceProvider ServiceProvider { get; }

        public AccountServiceTestSetup()
        {
            Mock<IUnitOfWork> mockUnitOfWork = DatabaseFake.DatabaseFakeSetup();
            ServiceProvider = DatabaseFake
                .ServiceFakeSetup(mockUnitOfWork.Object)
                .BuildServiceProvider();
        }
    }
}
