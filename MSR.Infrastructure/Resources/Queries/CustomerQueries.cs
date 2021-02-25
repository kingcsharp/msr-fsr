using Microsoft.EntityFrameworkCore;
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
        public static ApiPagingModel<CustomerModel> GetCustomersView(this DbSet<Customer> dbSet, ApiPagingModel<CustomerModel> paging, int? Id, string name, string address, string phone, int? primaryContactUserId, int? secondaryContactUserId, int? locationId, bool? isActive)
        {
            var customerList = new List<CustomerModel>();

            var customers = dbSet.Include(i => i.Location).Include(i => i.PrimaryContactUser).Include(i => i.SecondaryContactUser).AsQueryable();

            if (Id != null)
            {
                customers = customers.Where(i => i.Id == Id);
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                customers = customers.Where(i => i.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                customers = customers.Where(i => i.Address ==address);
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                customers = customers.Where(i => i.Phone == phone);
            }
            if (primaryContactUserId.HasValue)
            {
                customers = customers.Where(i => i.PrimaryContactUserId == primaryContactUserId.Value);
            }
            if (secondaryContactUserId.HasValue)
            {
                customers = customers.Where(i => i.SecondaryContactUserId == secondaryContactUserId.Value);
            }
            if (locationId.HasValue)
            {
                customers = customers.Where(i => i.LocationId == locationId.Value);
            }
            if (isActive.HasValue)
            {
                customers = customers.Where(i => i.IsActive == isActive.Value);
            }

            var pagedData = (paging as PagingModel).SortPaginate(customers);
            paging.Results = pagedData.Select(i => AutoMapperHelper.Mapper.Map<CustomerModel>(i)).ToList();

            return paging;
        }
    }
}
