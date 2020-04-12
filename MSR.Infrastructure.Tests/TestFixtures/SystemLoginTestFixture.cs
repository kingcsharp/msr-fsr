using MSR.Domain.Commands;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public class SystemLoginTestFixture
    {
        public static SystemLogin SuccessCommand => new SystemLogin() { Password = Constants.GoodPassword, UserName = Constants.GoodUserName };
        public static SystemLogin FailCommand => new SystemLogin() { Password = Constants.FailPassword, UserName = Constants.FailUserName };
        public static SystemLogin ExceptionCommand => new SystemLogin() { Password = Constants.ExceptionPassword, UserName = Constants.ExceptionUserName };
    }
}
