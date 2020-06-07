
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public static class RoleFixture
    {
        public static Role SuccessRole => new Role()
        {
            Name = "SuccessRole",
            Id = 1
        };
    }
}
