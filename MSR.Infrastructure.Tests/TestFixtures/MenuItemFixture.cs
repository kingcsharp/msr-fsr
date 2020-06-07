using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public static class MenuItemFixture
    {
        public static MenuItem SuccessMenuItem => new MenuItem()
        {
            Id = 1,
            Name = "SuccessMenuItem",
            OrderNumber = 1
        };
    }
}
