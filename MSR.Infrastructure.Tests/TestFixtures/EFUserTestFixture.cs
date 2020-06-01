using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public static class EFUserTestFixture
    {
        public static User GoodUser => new User()
        {
            UserName = Constants.GoodUserName
        };
    }
}
