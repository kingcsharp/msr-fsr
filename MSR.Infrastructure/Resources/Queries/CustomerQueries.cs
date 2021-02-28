using Microsoft.EntityFrameworkCore;
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
        public static ICollection<CustomerModel> CreateCustomerQuery(this IQueryable<Customer> query, GetMultipleCustomers command)
        {
            var customerList = new List<CustomerModel>();

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
                query = query.Where(i => i.PrimaryContactUser.GetFullName() == command.PrimaryContactUserName);
            }

            if (!string.IsNullOrEmpty(command.SecondartContactUserName))
            {
                query = query.Where(i => i.PrimaryContactUser.GetFullName() == command.SecondartContactUserName);
            }

            if (!string.IsNullOrEmpty(command.LocationName))
            {
                query = query.Where(i => i.Location.Name == command.LocationName);
            }

            if (!string.IsNullOrEmpty(command.CustomerNumber))
            {
                query = query.Where(i => i.CustomerNumber == command.CustomerNumber);
            }

            if (command.CreatedById.HasValue)
            {
                query = query.Where(i => i.CreatedBy == command.CreatedById.Value);
            }

            if (command.CreatedOn.HasValue)
            {
                query = query.Where(i => i.CreatedOn == command.CreatedOn.Value);
            }

            var customerModels = query.Select(i => AutoMapperHelper.Mapper.Map<CustomerModel>(i));
            
            return customerModels.ToList();
        }
    }
}
