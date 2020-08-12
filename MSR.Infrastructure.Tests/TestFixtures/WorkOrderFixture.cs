using MSR.Domain.Commands;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public static class WorkOrderFixture
    {
        public static GetWorkOrder AllWorkOrders => new GetWorkOrder(){ Id = null };
        public static GetWorkOrder NotFoundWorkOrder => new GetWorkOrder(){ Id = 31337 };
        public static UpdateWorkOrder WorkOrderUpdate => new UpdateWorkOrder(){ Id = 1, Price = 234.56M };
        public static CreateWorkOrder WorkOrderCreate => new CreateWorkOrder(){ Price = 456.78M };
        public static DeleteWorkOrder WorkOrderDelete => new DeleteWorkOrder(){ Id = 1 };
        public static WorkOrder PlainWorkOrder => new WorkOrder()
        {
            Id = 1,
            Price = 123.45M
        };
    }
}
