using MSR.Infrastructure.Resources.Services.Account.Abstractions;
using MSR.Infrastructure.Tests.ClassFixtures.Resources.Services;
using MSR.Infrastructure.Tests.TestFixtures;
using System.Threading.Tasks;
using Xunit;

namespace MSR.Infrastructure.Tests.Resources.Services
{
    public class AccountServiceTests: IClassFixture<AccountServiceTestSetup>
    {
        private readonly IAccountService _accountService;

        public AccountServiceTests(AccountServiceTestSetup testSetup)
        {
            _accountService = (IAccountService)testSetup.ServiceProvider.GetService(typeof(IAccountService));
        }

        public async Task CallingLogin_WithSystemLoginCommand_HappyPath()
        {
            var response = await _accountService.LoginAsync(SystemLoginTestFixture.SuccessCommand);


        }
    }
}
