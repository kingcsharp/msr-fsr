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
using System.Linq;
using System.Threading.Tasks;
using MSR.Domain.DTOs;
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
        public async Task Call_CreateWorkOrderAsync()
        {
            var workOrderDto = CreateWorkOrderDTO.FromCommand(WorkOrderFixture.WorkOrderCreate);
            var response = await _workorderService.CreateWorkOrderAsync(workOrderDto);
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
