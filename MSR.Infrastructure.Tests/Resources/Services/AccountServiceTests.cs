using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Tests.ClassFixtures.Resources.Services;
using MSR.Infrastructure.Tests.TestFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MSR.Infrastructure.Tests.Resources.Services
{
    public class AccountServiceTests: IClassFixture<AccountServiceTestSetup>
    {
        private readonly IAccountService _accountService;

        public AccountServiceTests(AccountServiceTestSetup testSetup)
        {
            _accountService = testSetup.ServiceProvider.GetService<IAccountService>();
        }

        [Fact]
        public async Task CallingLogin_WithSystemLoginCommand_HappyPath()
        {
            var response = await _accountService.LoginAsync(SystemLoginFixture.SuccessCommand);

            response.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void CallingLogin_WithFailureSystemLoginCommand_ThrowsDomainException()
        {
            Func<Task<string>> response = () => _accountService.LoginAsync(SystemLoginFixture.FailCommand);

            response.Should().Throw<DomainException>();
        }

        [Fact]
        public void CallingLogin_WithExceptionSystemLoginCommand_ThrowsArgumentException()
        {
            Func<Task<string>> response = () => _accountService.LoginAsync(SystemLoginFixture.ExceptionCommand);

            response.Should().Throw<DomainException>();
        }
    }
}
