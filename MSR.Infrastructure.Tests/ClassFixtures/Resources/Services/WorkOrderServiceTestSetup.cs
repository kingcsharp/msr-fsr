using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using MSR.Domain.Abstractions.Services;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using MSR.Infrastructure.Resources.Services.Role;
using MSR.Infrastructure.Tests.TestFixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
                new WorkOrder() { Price = 123.45M }
            };
            var workOrdersMock = workOrdersList.AsQueryable().BuildMock();
            workOrders.Setup(m => m.Query()).Returns(workOrdersMock.Object);
            mockUnitOfWork.SetupGet(m => m.WorkOrders)
                .Returns(workOrders.Object);

            var services = DatabaseFake.ServiceFakeSetup(mockUnitOfWork.Object);
            services.AddScoped<IWorkOrderService,WorkOrderService>();
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
