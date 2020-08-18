using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using MSR.Infrastructure.Resources.Services.Role;
using MSR.Infrastructure.Tests.TestFixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Tests.ClassFixtures.Resources.Services
{
    public class WorkOrderServiceTestSetup
    {
        public ServiceProvider ServiceProvider { get; }

        public WorkOrderServiceTestSetup()
        {
            var mockUnitOfWork = DatabaseFake.DatabaseFakeSetup();

            var workOrders = new Mock<IRepository<WorkOrder>>();
            var workOrdersList = new List<WorkOrder>() { 
                WorkOrderFixture.PlainWorkOrder
            };
            var workOrdersMock = workOrdersList.AsQueryable().BuildMock();
            workOrders.Setup(m => m.Query()).Returns(workOrdersMock.Object);
            workOrders.Setup(m => m.FirstOrDefaultAsync(It.IsAny<bool>(),
                It.IsAny<Expression<Func<WorkOrder, bool>>>())
            ).Returns(Task.FromResult(workOrdersList[0]));
            mockUnitOfWork.SetupGet(m => m.WorkOrders)
                .Returns(workOrders.Object);

            CurrentUser.HasPrivilege = (EnumMenuItem, EnumPrivilege) => { return true; };
            CurrentUser.GetId = () => { return 1; };

            var services = DatabaseFake.ServiceFakeSetup(mockUnitOfWork.Object);
            services.AddScoped<IWorkOrderService,WorkOrderService>();
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
