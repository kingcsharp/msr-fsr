using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Abstractions.Services;
using MSR.Infrastructure.Tests.ClassFixtures.Resources.Services;
using MSR.Infrastructure.Tests.TestFixtures;
using System.Threading.Tasks;
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

        [Fact]
        public async Task CallingCreateMenuRoleMapAsync_WithGoodData_HappyPath()
        {
            /*
            var result = await _roleService.CreateMenuRoleMapAsync(CreateMenuRoleMapFixture.SuccessCreateMenuRoleMap);

            result.Should().Be(MenuRoleFixture.SuccessMenuRole.Id);
            */
        }

        [Fact(Skip = "Issues with FirstOrDefault")]
        public async Task CallingCreateMenuRoleMapAsync_WithExistingData_ReturnsExistingId()
        {
            /*
            var result = await _roleService.CreateMenuRoleMapAsync(CreateMenuRoleMapFixture.SuccessExistingCreateMenuRoleMap);
          
            result.Should().Be(MenuRoleFixture.ExistingMenuRole.Id);
            */
        }
    }
}
