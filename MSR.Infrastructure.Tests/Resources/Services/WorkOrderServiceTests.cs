using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Tests.ClassFixtures.Resources.Services;
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
        public async Task Call_GetWorkOrderAsync_NotFound()
        {
            Func<Task<ICollection<WorkOrderModel>>> response = () => _workorderService.GetWorkOrderAsync(new GetWorkOrder() { Id = 123 });
            response.Should().Throw<DomainException>();
        }

        [Fact]
        public async Task Call_GetWorkOrderAsync_Found()
        {
            var response = await _workorderService.GetWorkOrderAsync(new GetWorkOrder() { Id = null });
            response.Should().HaveCount(1);
        }

        [Fact]
        public async Task Call_CreateWorkOrderAsync()
        {
            var response = await _workorderService.CreateWorkOrderAsync(new CreateWorkOrder());
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Call_UpdateWorkOrderAsync()
        {
            var response = await _workorderService.UpdateWorkOrderAsync(new UpdateWorkOrder() {
                Id = 1,
                Price = 234.56M
            });
            response.Should().Match<WorkOrderModel>(x => (
                x.Id == 1 &&
                x.Price == 234.56M
            ));
        }

        [Fact]
        public async Task Call_DeleteWorkOrderAsync()
        {

        }

    }
}
