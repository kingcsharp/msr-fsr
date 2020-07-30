using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Abstractions.Services;
using MSR.Infrastructure.Tests.ClassFixtures.Resources.Services;
using Xunit;

namespace MSR.Infrastructure.Tests.Resources.Services
{
    public class RoleServiceTests: IClassFixture<RoleServiceTestSetup>
    {
        private readonly IRoleService _roleService;

        public RoleServiceTests(RoleServiceTestSetup setup)
        {
            _roleService = setup.ServiceProvider.GetService<IRoleService>();
        }
    }
}
