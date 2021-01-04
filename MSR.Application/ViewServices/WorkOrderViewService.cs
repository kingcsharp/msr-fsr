using AutoMapper;
using MSR.Application.Abstractions;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Projections;
using MSR.Infrastructure.Resources.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MSR.Application.ViewServices
{
    public class WorkOrderViewService : IWorkOrderViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WorkOrderViewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<InvoiceableWorkOrderView>> GetInvoiceableWorkOrders()
        {
            List<int> invoicedWorkOrderIds = await _unitOfWork.InvoiceItems.Query().Select(i => i.WorkOrderId).Distinct().ToListAsync();
            
            return await _unitOfWork.Query<WorkOrder>().GetInvoiceableWorkOrders(WorkOrderProjections.InvoiceableWorkOrderView, invoicedWorkOrderIds);

        }
    }
}
