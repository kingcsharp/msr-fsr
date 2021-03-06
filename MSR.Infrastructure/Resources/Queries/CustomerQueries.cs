using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Models.Paging;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class CustomerQueries
    {
        public static IQueryable<CustomerModel> CreateCustomerQuery(this IQueryable<CustomerModel> query, GetMultipleCustomers command, bool forRowCount = false)
        {

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));
            query = query.Where(command.Address, s => s.Address.Contains(command.Address));
            query = query.Where(command.Phone, s => s.Phone.Contains(command.Phone));
            query = query.Where(command.PrimaryContactUserId, s => s.PrimaryContactUser.Id == command.PrimaryContactUserId);
            query = query.Where(command.SecondaryContactUserId, s => s.SecondaryContactUser.Id == command.SecondaryContactUserId);
            query = query.Where(command.LocationId, s=> s.Location.Id == command.LocationId);
            query = query.Where(command.IsActive, s => s.IsActive == command.IsActive);
            query = query.Where(command.PrimaryContactUserFullName, s => s.PrimaryContactUser.FullName.Contains(command.PrimaryContactUserFullName));
            query = query.Where(command.SecondaryContactUserFullName, s => s.SecondaryContactUser.FullName.Contains(command.SecondaryContactUserFullName));
            query = query.Where(command.LocationName, s => s.Location.Name.Contains(command.LocationName));
            query = query.Where(command.CustomerNumber, s=> s.CustomerNumber.Contains(command.CustomerNumber));
            query = query.Where(command.CreatedFullName, s=> s.Created.FullName.Contains(command.CreatedFullName));
            query = query.Where(command.CreatedOn, s=> DateTime.Compare(s.CreatedOn.Date, command.CreatedOn.Value.Date) == 0);
            query = query.Where(command.Status, s => command.Status.Contains(s.Status));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Address), s => s.Address);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Phone), s => s.Phone);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.IsActive), s => s.IsActive);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.PrimaryContactUserFullName), s => s.PrimaryContactUser.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.SecondaryContactUserFullName), s => s.SecondaryContactUser.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.LocationName), s => s.Location.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.CustomerNumber), s => s.CustomerNumber);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.CreatedByFullName), s => s.Created.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Status), s => s.Status);

            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
