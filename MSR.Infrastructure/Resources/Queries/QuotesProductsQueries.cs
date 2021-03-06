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
            query = query.Where(command.Company, s => s.Company.Contains(command.Company));
            query = query.Where(command.CycleTime, s => s.CycleTime == command.CycleTime);
            query = query.Where(command.DivisionFab, s => s.DivisionFab.Contains(command.DivisionFab));
            query = query.Where(command.EquipmentCost, s => s.EquipmentCost == command.EquipmentCost);
            query = query.Where(command.LastUpdatedBy, s => s.LastUpdateBy.Contains(command.LastUpdatedBy));
            query = query.Where(command.LastUpdateOn, s => DateTime.Compare(s.LastUpdateOn.Value.Date,command.LastUpdateOn.Value.Date) == 0);
            query = query.Where(command.MaterialCost, s => s.MaterialCost == command.MaterialCost);
            query = query.Where(command.PartKitNo, s => s.PartKitNo.Contains(command.PartKitNo));
            query = query.Where(command.ProcedureName, s => s.ProcedureName.Contains(command.ProcedureName));
            query = query.Where(command.ProductName, s => s.ProductName.Contains(command.ProductName));
            query = query.Where(command.Representative, s => s.Representative.Contains(command.Representative));
            query = query.Where(command.Revision, s => s.Revision == command.Revision);
            query = query.Where(command.SalesTax, s => s.SalesTax == command.SalesTax);
            query = query.Where(command.SegregationType, s => EnumUtils.GetDescription(s.SegregationType).Contains(command.SegregationType));
            query = query.Where(command.SubmittedByFullName, s => s.SubmittedBy.FullName.Contains(command.SubmittedByFullName));
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
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumQuotesProductsSortFields.SubmittedByFullName), s => s.SubmittedBy.FullName);
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
