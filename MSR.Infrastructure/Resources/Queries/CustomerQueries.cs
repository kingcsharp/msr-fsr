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
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class CustomerQueries
    {
        public static IQueryable<CustomerModel> CreateCustomerQuery(this IQueryable<CustomerModel> query, GetMultipleCustomers command, bool forRowCount = false)
        {

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.Name, s => !string.IsNullOrEmpty(s.Name) && s.Name.Contains(command.Name));
            query = query.Where(command.Address,s => !string.IsNullOrEmpty(s.Address) && s.Address.Contains(command.Address));
            query = query.Where(command.Phone, s => !string.IsNullOrEmpty(s.Phone) && s.Phone.Contains(command.Phone));
            query = query.Where(command.PrimaryContactUserId, s => s.PrimaryContactUser.Id == command.PrimaryContactUserId);
            query = query.Where(command.SecondaryContactUserId, s => s.SecondaryContactUser.Id == command.SecondaryContactUserId);
            query = query.Where(command.LocationId, s=> s.Location.Id == command.LocationId);
            query = query.Where(command.IsActive, s => s.IsActive == command.IsActive);
            query = query.Where(command.PrimaryContactUserFullName, s => s.PrimaryContactUser != null && !string.IsNullOrEmpty(s.PrimaryContactUser.FullName) && s.PrimaryContactUser.FullName.Contains(command.PrimaryContactUserFullName));
            query = query.Where(command.SecondaryContactUserFullName, s => s.SecondaryContactUser != null && !string.IsNullOrEmpty(s.SecondaryContactUser.FullName) && s.SecondaryContactUser.FullName.Contains(command.SecondaryContactUserFullName));
            query = query.Where(command.LocationName, s => s.Location != null && !string.IsNullOrEmpty(s.Location.Name) && s.Location.Name.Contains(command.LocationName));
            query = query.Where(command.CustomerNumber, s=> !string.IsNullOrEmpty(s.CustomerNumber) && s.CustomerNumber.Contains(command.CustomerNumber));
            query = query.Where(command.CreatedFullName, s=> !string.IsNullOrEmpty(s.Created.FullName) && s.Created.FullName.Contains(command.CreatedFullName));
            query = query.Where(command.CreatedOn, s=> DateTime.Compare(s.CreatedOn.Date, command.CreatedOn.Value.Date) == 0);

            if(command.Status == null) {
                query = query.Where(command.Status, s => s.Status == null);
            } else {
                query = query.Where(command.Status, s => string.IsNullOrEmpty(s.Status) || command.Status.Select(m => m.ToLower()).Contains(s.Status.ToLower()));
            }
            

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Address), s => s.Address);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.Phone), s => s.Phone);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.IsActive), s => s.IsActive);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.PrimaryContactUserFullName), s => s.PrimaryContactUser == null ? "" : s.PrimaryContactUser.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.SecondaryContactUserFullName), m => m.SecondaryContactUser == null ? "" : m.SecondaryContactUser.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.LocationName), s => s.Location == null ? "" : s.Location.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.CustomerNumber), s => s.CustomerNumber);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumCustomerSortFields.CreatedByFullName), s => s.Created == null ? "" : s.Created.FullName);
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
