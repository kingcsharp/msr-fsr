using MSR.Infrastructure.Tests.ClassFixtures.Resources.Services;
using MSR.Infrastructure.Tests.TestFixtures;
using System.Threading.Tasks;
using MSR.Domain.Abstractions.Services;
using Xunit;
using FluentAssertions;
using System;
using MSR.Domain.Exceptions;

namespace MSR.Infrastructure.Tests.Resources.Services
{
    public class AccountServiceTests: IClassFixture<AccountServiceTestSetup>
    {
        private readonly IAccountService _accountService;

        public AccountServiceTests(AccountServiceTestSetup testSetup)
        {
            _accountService = (IAccountService)testSetup.ServiceProvider.GetService(typeof(IAccountService));
        }

        [Fact]
        public async Task CallingLogin_WithSystemLoginCommand_HappyPath()
        {
            var response = await _accountService.LoginAsync(SystemLoginTestFixture.SuccessCommand);

            response.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task CallingLogin_WithFailureSystemLoginCommand_ThrowsDomainException()
        {
            Func<Task<string>> response = () => _accountService.LoginAsync(SystemLoginTestFixture.FailCommand);

            response.Should().Throw<DomainException>();
        }


        [Fact]
        public async Task CallingLogin_WithExceptionSystemLoginCommand_ThrowsArgumentException()
        {
            Func<Task<string>> response = () => _accountService.LoginAsync(SystemLoginTestFixture.ExceptionCommand);

            response.Should().Throw<ArgumentException>();
        }
    }
}
