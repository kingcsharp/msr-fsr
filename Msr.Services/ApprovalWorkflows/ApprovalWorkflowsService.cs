using System;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Models.ApprovalWorkflows;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.Companies.Procedures;
using Msr.Services.Companies.ViewModels;

namespace Msr.Services.ApprovalWorkflows
{
    public class ApprovalWorkflowsService
    {
        private readonly MsrDbContext _dbContext;

        public ApprovalWorkflowsService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ApprovalWorkflowsView> GetApprovalWorkflowsQueryable()
        {
            return _dbContext.ApprovalWorkflowsViews;
        }

    }
}
