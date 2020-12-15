using MSR.Application.Abstractions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Projections;
using MSR.Infrastructure.Resources.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Application.ViewServices
{
    public class WorkOrderViewService : IWorkOrderViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkOrderViewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<dynamic> GetInvoiceableWorkOrders()
        {
            return _unitOfWork.Query<WorkOrder>().GetInvoiceableWorkOrders(WorkOrderProjections.InvoiceableWorkOrderView);
        }
    }
}
