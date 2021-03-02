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

            if (command.Id.HasValue)
            {
                query = query.Where(i => i.Id == command.Id.Value);
            }
            if (!string.IsNullOrWhiteSpace(command.Name))
            {
                query = query.Where(i => i.Name == command.Name);
            }
            if (!string.IsNullOrWhiteSpace(command.Address))
            {
                query = query.Where(i => i.Address == command.Address);
            }
            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                query = query.Where(i => i.Phone == command.Phone);
            }
            if (command.PrimaryContactUserId.HasValue)
            {
                query = query.Where(i => i.PrimaryContactUserId == command.PrimaryContactUserId.Value);
            }
            if (command.SecondaryContactUserId.HasValue)
            {
                query = query.Where(i => i.SecondaryContactUserId == command.SecondaryContactUserId.Value);
            }
            if (command.LocationId.HasValue)
            {
                query = query.Where(i => i.LocationId == command.LocationId.Value);
            }
            if (command.IsActive.HasValue)
            {
                query = query.Where(i => i.IsActive == command.IsActive.Value);
            }

            if (!string.IsNullOrEmpty(command.PrimaryContactUserName))
            {
                query = query.Where(i => i.PrimaryContactUser.FirstName == command.PrimaryContactUserName);
            }

            if (!string.IsNullOrEmpty(command.SecondartContactUserName))
            {
                query = query.Where(i => i.PrimaryContactUser.FirstName == command.SecondartContactUserName);
            }

            if (!string.IsNullOrEmpty(command.LocationName))
            {
                query = query.Where(i => i.Location.Name == command.LocationName);
            }

            if (!string.IsNullOrEmpty(command.CustomerNumber))
            {
                query = query.Where(i => i.CustomerNumber == command.CustomerNumber);
            }

            if (!string.IsNullOrEmpty(command.CreatedByFullName))
            {
                query = query.Where(i => i.Created.FirstName == command.CreatedByFullName);
            }

            if (command.CreatedOn.HasValue)
            {
                query = query.Where(i => i.CreatedOn == command.CreatedOn.Value);
            }

            if(command.SortAscending.HasValue && command.SortAscending.Value && !string.IsNullOrEmpty(command.Term)) { 
            
                if(command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.Id)) { 
                    
                    query = query.OrderBy(s => s.Id);
                    
                } else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.Address)) {

                    query = query.OrderBy(s => s.Address);

                } else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.Phone))
                {

                    query = query.OrderBy(s => s.Phone);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.IsActive))
                {

                    query = query.OrderBy(s => s.IsActive);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.PrimaryContactUserName))
                {

                    query = query.OrderBy(s => s.PrimaryContactUser.FirstName);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.SecondartContactUserName))
                {

                    query = query.OrderBy(s => s.SecondaryContactUser.FirstName);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.LocationName))
                {

                    query = query.OrderBy(s => s.Location.Name);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.CustomerNumber))
                {

                    query = query.OrderBy(s => s.CustomerNumber);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.CreatedOn))
                {

                    query = query.OrderBy(s => s.CreatedOn);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.CreatedByFullName))
                {

                    query = query.OrderBy(s => s.Created.FirstName);

                }

            }

            if (command.SortAscending.HasValue && !command.SortAscending.Value && !string.IsNullOrEmpty(command.Term))
            {

                if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.Id))
                {

                    query = query.OrderByDescending(s => s.Id);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.Address))
                {

                    query = query.OrderByDescending(s => s.Address);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.Phone))
                {

                    query = query.OrderByDescending(s => s.Phone);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.IsActive))
                {

                    query = query.OrderByDescending(s => s.IsActive);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.PrimaryContactUserName))
                {

                    query = query.OrderByDescending(s => s.PrimaryContactUser.FirstName);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.SecondartContactUserName))
                {

                    query = query.OrderByDescending(s => s.SecondaryContactUser.FirstName);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.LocationName))
                {

                    query = query.OrderByDescending(s => s.Location.Name);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.CustomerNumber))
                {

                    query = query.OrderByDescending(s => s.CustomerNumber);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.CreatedOn))
                {

                    query = query.OrderByDescending(s => s.CreatedOn);

                }
                else if (command.Term == EnumUtils.GetDescription<EnumCustomerSortFields>(EnumCustomerSortFields.CreatedByFullName))
                {

                    query = query.OrderByDescending(s => s.Created.FirstName);

                }

            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }
            
            return query;
        }
    }
}
