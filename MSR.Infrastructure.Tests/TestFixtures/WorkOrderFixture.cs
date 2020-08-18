using Bogus;
using MSR.Domain.Commands;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public static class WorkOrderFixture
    {
        public static int orderid = 1;
        public static WorkOrder PlainWorkOrder =>
            new Faker<WorkOrder>()
                .RuleFor(o => o.Id, f => orderid++)
                .RuleFor(o => o.Price, f => f.Random.Decimal(0,1000))
                .Generate();

        public static GetWorkOrder AllWorkOrders =>
            new GetWorkOrder(){ Id = null };
        public static GetWorkOrder NotFoundWorkOrder =>
            new Faker<GetWorkOrder>()
                .RuleFor(o => o.Id, f => orderid++)
                .Generate();
        public static UpdateWorkOrder WorkOrderUpdate =>
            new Faker<UpdateWorkOrder>()
                .RuleFor(x => x.Id, f => PlainWorkOrder.Id)
                .RuleFor(x => x.Price, f => f.Random.Decimal(0,1000))
                .Generate();
        public static CreateWorkOrder WorkOrderCreate =>
            new Faker<CreateWorkOrder>()
                .RuleFor(o => o.Price, f => f.Random.Decimal(0,1000))
                .Generate();
        public static DeleteWorkOrder WorkOrderDelete =>
            new DeleteWorkOrder(){ Id = PlainWorkOrder.Id };
    }
}
