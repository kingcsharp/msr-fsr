using FluentAssertions;
using MSR.Infrastructure.Helpers;
using MSR.Infrastructure.Helpers.Abstractions;
using System.Text;
using Xunit;

namespace MSR.Infrastructure.Tests
{
    public class AuthenticationHelperTests
    {
        private readonly IAuthenticationHelper _authenticationHelper;

        public AuthenticationHelperTests()
        {
            _authenticationHelper = new AuthenticationHelper();
        }

        public void CallingCreatePassword_WithGoodData_HappyPath()
        {
            _authenticationHelper.CreatePasswordHash(Constants.GoodPassword, out var hash, out var salt);

            var hashStr = Encoding.Default.GetString(hash);
            var saltStr = Encoding.Default.GetString(salt);

            hash.Should().BeEquivalentTo(Constants.GoodHash);
            salt.Should().BeEquivalentTo(Constants.GoodSalt);
        }
    }
}
