using MSR.Domain.Commands;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public static class CreateMenuRoleMapFixture
    {
        public static CreateMenuRoleMap SuccessCreateMenuRoleMap => new CreateMenuRoleMap()
        {
            MenuId = 1,
            RoleId = 1
        };
        
        public static CreateMenuRoleMap SuccessExistingCreateMenuRoleMap => new CreateMenuRoleMap()
        {
            MenuId = 2,
            RoleId = 2
        };

        public static CreateMenuRoleMap ExceptionCreateMenuRoleMap => new CreateMenuRoleMap()
        {
            MenuId = 3,
            RoleId = 3
        };
    }
}
