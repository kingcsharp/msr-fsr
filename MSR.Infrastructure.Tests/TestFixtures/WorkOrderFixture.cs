using Bogus;
using MSR.Domain.Commands;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public static class WorkOrderFixture
    {
        public static int orderid = 1;
        public static int purchaseid = new Randomizer().Int(1,10);
        public static string cpn = new Randomizer().Replace("*********");
        public static Purchase PlainPurchase =>
            new Faker<Purchase>()
                .RuleFor(o => o.Id, f => purchaseid)
                .RuleFor(o => o.CustomerLineNumber, f => f.Random.Int(1,999))
                .RuleFor(o => o.CustomerPurchaseNumber, cpn)
                .Generate();
        public static WorkOrder PlainWorkOrder =>
            new Faker<WorkOrder>()
                .RuleFor(o => o.Id, f => orderid++)
                .RuleFor(o => o.Price, f => f.Random.Decimal(0,1000))
                .RuleFor(o => o.Purchase, f => PlainPurchase)
                .RuleFor(o => o.PurchaseId, f => purchaseid)
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
