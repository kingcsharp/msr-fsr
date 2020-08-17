using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Tests.ClassFixtures.Resources.Services;
using MSR.Infrastructure.Tests.TestFixtures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MSR.Infrastructure.Tests.Resources.Services
{
    public class WorkOrderServiceTests: IClassFixture<WorkOrderServiceTestSetup>
    {
        private readonly IWorkOrderService _workorderService;

        public WorkOrderServiceTests(WorkOrderServiceTestSetup testSetup)
        {
            _workorderService = testSetup.ServiceProvider.GetService<IWorkOrderService>();
        }

        [Fact]
        public Task Call_GetWorkOrderAsync_NotFound()
        {
            Func<Task<ICollection<WorkOrderModel>>> response = () =>
                _workorderService.GetWorkOrderAsync(WorkOrderFixture.NotFoundWorkOrder);
            return Task.FromResult(response.Should().Throw<DomainException>());
        }

        [Fact]
        public async Task Call_GetWorkOrderAsync_Found()
        {
            var response = await _workorderService.GetWorkOrderAsync(
                WorkOrderFixture.AllWorkOrders
            );
            response.Should().HaveCount(1);
        }

        [Fact]
        public async Task Call_CreateWorkOrderAsync()
        {
            var response = await _workorderService.CreateWorkOrderAsync(
                WorkOrderFixture.WorkOrderCreate
            );
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Call_UpdateWorkOrderAsync()
        {
            UpdateWorkOrder upd = WorkOrderFixture.WorkOrderUpdate;
            var response = await _workorderService.UpdateWorkOrderAsync(upd);
            response.Should().Match<WorkOrderModel>(x => (
                x.Id == upd.Id &&
                x.Price == upd.Price
            ));
        }

        [Fact]
        public async Task Call_DeleteWorkOrderAsync()
        {
            var response = await _workorderService.DeleteWorkOrderAsync(
                WorkOrderFixture.WorkOrderDelete
            );
            response.Should().BeTrue();
        }

    }
}
