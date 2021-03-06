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
        public static IQueryable<Customer> CreateCustomerQuery(this IQueryable<Customer> query, GetMultipleCustomers command, bool forRowCount = false)
        {

            query = query.Include(i => i.Location).Include(i => i.PrimaryContactUser).Include(i => i.SecondaryContactUser).AsQueryable();

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));
            query = query.Where(command.Address, s => s.Name.Contains(command.Address));
            query = query.Where(command.Phone, s => s.Phone.Contains(command.Phone));
            query = query.Where(command.PrimaryContactUserId, s => s.PrimaryContactUser.Id == command.PrimaryContactUserId);
            query = query.Where(command.SecondaryContactUserId, s => s.SecondaryContactUser.Id == command.SecondaryContactUserId);
            query = query.Where(command.LocationId, s=> s.LocationId == command.LocationId);
            query = query.Where(command.IsActive, s => s.IsActive == command.IsActive);
            query = query.Where(command.PrimaryContactUserName, s => s.PrimaryContactUser.FullName.Contains(command.PrimaryContactUserName));
            query = query.Where(command.SecondartContactUserName, s => s.SecondaryContactUser.FullName.Contains(command.SecondartContactUserName));
            query = query.Where(command.LocationName, s => s.Location.Name.Contains(command.LocationName));
            query = query.Where(command.CustomerNumber, s=> s.CustomerNumber.Contains(command.CustomerNumber));
            query = query.Where(command.CreatedByFullName, s=> s.Created.FullName.Contains(command.CreatedByFullName));
            query = query.Where(command.CreatedOn, s=> DateTime.Compare(s.CreatedOn.Date, command.CreatedOn.Value.Date) == 0);

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Address), s => s.Address);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Phone), s => s.Phone);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.IsActive), s => s.IsActive);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.PrimaryContactUserName), s => s.PrimaryContactUser.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.SecondartContactUserName), s => s.SecondaryContactUser.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.LocationName), s => s.Location.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.CustomerNumber), s => s.CustomerNumber);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.CreatedByFullName), s => s.Created.FullName);

            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
