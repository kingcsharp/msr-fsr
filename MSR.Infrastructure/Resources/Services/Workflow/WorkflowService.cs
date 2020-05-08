using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Workflow
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkflowService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PendingApprovalNotification> GetApprovalNotificationsAsync(GetPendingApprovals command)
        {
            //TODO: Make this less brittle
            var inProcressStatusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == "In Progress").Id;
            var pendingStatusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == "Pending").Id;

            return new PendingApprovalNotification()
            {
                Customers = _unitOfWork.CustomerApprovals.Count(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId),
                Locations = _unitOfWork.LocationApprovals.Count()
            };
        }
    }
}
