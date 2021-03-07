using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class QuotesProductsQueries
    {
        public static IQueryable<QuotesProductsView> CreateQuotesProductsQuery(this IQueryable<QuotesProductsView> query, GetQuotesProducts command, bool forRowCount = false)
        {
            query = query.Where(command.Company, s => !string.IsNullOrEmpty(s.Company) && s.Company.ToLower().Contains(command.Company.ToLower()));
            query = query.Where(command.CycleTime, s => s.CycleTime == command.CycleTime);
            query = query.Where(command.DivisionFab, s => !string.IsNullOrEmpty(s.DivisionFab) && s.DivisionFab.ToLower().Contains(command.DivisionFab.ToLower()));
            query = query.Where(command.EquipmentCost, s => s.EquipmentCost == command.EquipmentCost);
            query = query.Where(command.LastUpdatedBy, s => !string.IsNullOrEmpty(s.LastUpdateBy) && s.LastUpdateBy.ToLower().Contains(command.LastUpdatedBy.ToLower()));
            query = query.Where(command.LastUpdateOn, s => DateTime.Compare(s.LastUpdateOn.Value.Date,command.LastUpdateOn.Value.Date) == 0);
            query = query.Where(command.MaterialCost, s => s.MaterialCost == command.MaterialCost);
            query = query.Where(command.PartKitNo, s => !string.IsNullOrEmpty(s.PartKitNo) && s.PartKitNo.ToLower().Contains(command.PartKitNo.ToLower()));
            query = query.Where(command.ProcedureName, s => !string.IsNullOrEmpty(s.ProcedureName) && s.ProcedureName.ToLower().Contains(command.ProcedureName.ToLower()));
            query = query.Where(command.ProductName, s => !string.IsNullOrEmpty(s.ProductName) && s.ProductName.ToLower().Contains(command.ProductName.ToLower()));
            query = query.Where(command.Representative, s => !string.IsNullOrEmpty(s.Representative) && s.Representative.ToLower().Contains(command.Representative.ToLower()));
            query = query.Where(command.Revision, s => s.Revision == command.Revision);
            query = query.Where(command.SalesTax, s => s.SalesTax == command.SalesTax);
            query = query.Where(command.SegregationType, s => s.SegregationType.HasValue && command.SegregationType.ToList().Contains(s.SegregationType.Value));
            query = query.Where(command.SubmittedByFullName, s => s.SubmittedBy != null && s.SubmittedBy.FullName.ToLower().Contains(command.SubmittedByFullName.ToLower()));
            query = query.Where(command.TotalPrice, s => s.TotalPrice == command.TotalPrice);
            query = query.Where(command.SubmittedDate, s => DateTime.Compare(s.SubmittedDate.Date, command.SubmittedDate.Value.Date) == 0);

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.Company), s => s.Company);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.CycleTime), s => s.CycleTime);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.DivisionFab), s => s.DivisionFab);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.EquipmentCost), s => s.EquipmentCost);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.LastUpdatedBy), s => s.LastUpdateBy);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.LastUpdateOn), s => s.LastUpdateOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.MaterialCost), s => s.MaterialCost);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.PartKitNo), s => s.PartKitNo);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.ProcedureName), s => s.ProcedureName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.ProductName), s => s.ProductName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.Representative), s => s.Representative);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.Revision), s => s.Revision);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.SalesTax), s => s.SalesTax);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.SegregationType), s => s.SegregationType);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.SubmittedByFullName), s => s.SubmittedBy == null ? "" : s.SubmittedBy.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.SubmittedDate), s => s.SubmittedDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.TotalPrice), s => s.TotalPrice);

            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
