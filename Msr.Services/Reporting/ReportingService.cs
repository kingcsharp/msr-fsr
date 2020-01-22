using Msr.Models.Reporting;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Reporting
{
    public class ReportingService
    {
        private readonly MsrDbContext _dbContext;

        public ReportingService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<CombinedFinancialData> GetCombinedFinancialData()
        {
            return _dbContext.CombinedFinancialData.AsQueryable();
        }

        public IQueryable<WorkOrdersWithoutInvoices> GetWorkOrdersWithoutInvoices()
        {
            return _dbContext.WorkOrdersWithoutInvoices.AsQueryable();
        }

        public IQueryable<ActualPartsHistory> GetActualPartsHistory()
        {
            return _dbContext.ActualPartsHistory.AsQueryable();
        }

        public IQueryable<SerialNumberHistory> GetSerialNumberHistory()
        {
            return _dbContext.SerialNumberHistory.AsQueryable();
        }

        public IQueryable<WorkInProcess> GetWorkInProcess()
        {
            return _dbContext.WorkInProcess.AsQueryable();
        }

        public IQueryable<OperationsData> GetRevenueByCustomerData()
        {
            return _dbContext.OperationsData.AsQueryable();
        }
    }
}