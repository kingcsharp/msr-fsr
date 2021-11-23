using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.QueryFilters;
using MSR.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class ProductQueries
    {
        public static IQueryable<ProductDownloadViewFilter> CreateQuotesProductsQuery(this IQueryable<ProductDownloadViewFilter> query, ProductDownloadFilter filters, bool forRowCount = false)
        {
            query = query.Where(filters.Company, s => !string.IsNullOrEmpty(s.Company) && s.Company.ToLower().Contains(filters.Company.ToLower()));
            query = query.Where(filters.CycleTime, s => s.CycleTime == filters.CycleTime);
            query = query.Where(filters.DivisionFab, s => !string.IsNullOrEmpty(s.DivisionFab) && s.DivisionFab.ToLower().Contains(filters.DivisionFab.ToLower()));
            query = query.Where(filters.EquipmentCost, s => s.EquipmentCost == filters.EquipmentCost);
            query = query.Where(filters.LastUpdatedBy, s => !string.IsNullOrEmpty(s.LastUpdatedBy) && s.LastUpdatedBy.ToLower().Contains(filters.LastUpdatedBy.ToLower()));
            query = query.Where(filters.LastUpdateOn, s => DateTime.Compare(s.LastUpdateOn.Value.Date, filters.LastUpdateOn.Value.Date) == 0);
            query = query.Where(filters.MaterialCost, s => s.MaterialCost == filters.MaterialCost);
            query = query.Where(filters.PartKitNo, s => !string.IsNullOrEmpty(s.PartKitNo) && s.PartKitNo.ToLower().Contains(filters.PartKitNo.ToLower()));
            query = query.Where(filters.ProcedureName, s => !string.IsNullOrEmpty(s.ProcedureName) && s.ProcedureName.ToLower().Contains(filters.ProcedureName.ToLower()));
            query = query.Where(filters.ProductName, s => !string.IsNullOrEmpty(s.ProductName) && s.ProductName.ToLower().Contains(filters.ProductName.ToLower()));
            query = query.Where(filters.Representative, s => !string.IsNullOrEmpty(s.Representative) && s.Representative.ToLower().Contains(filters.Representative.ToLower()));
            query = query.Where(filters.Revision, s => s.Revision == filters.Revision);
            query = query.Where(filters.SalesTax, s => s.SalesTax == filters.SalesTax);
            query = query.Where(filters.SegregationType, s => s.SegregationType.HasValue && filters.SegregationType.ToList().Contains(s.SegregationType.Value));
            query = query.Where(filters.SubmittedByFullName, s => s.SubmittedBy != null && s.SubmittedBy.ToLower().Contains(filters.SubmittedByFullName.ToLower()));
            query = query.Where(filters.TotalPrice, s => s.TotalPrice == filters.TotalPrice);
            query = query.Where(filters.SubmittedDate, s => DateTime.Compare(s.SubmittedDate.Date, filters.SubmittedDate.Value.Date) == 0);
            query = query.Where(filters.ProcedureId, s => s.ProcedureId == filters.ProcedureId);

           return query;
        }
    }
}
