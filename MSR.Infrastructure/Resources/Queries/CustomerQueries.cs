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
        public static (ICollection<CustomerModel> data, int totalRows) GetCustomersView(this DbSet<Customer> dbSet, int skip, int take, int? id, string name, string address, string phone, int? primaryContactUserId, int? secondaryContactUserId, int? locationId, bool? isActive)
        {
            var customerList = new List<CustomerModel>();

            var customers = dbSet.Include(i => i.Location).Include(i => i.PrimaryContactUser).Include(i => i.SecondaryContactUser).AsQueryable();

            if (id != null)
            {
                customers = customers.Where(i => i.Id == id);
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

            var pagedData = customers.Paginate(skip, take);
            var customerModels = pagedData.data.Select(i => AutoMapperHelper.Mapper.Map<CustomerModel>(i));

            return (customerModels.ToList(), pagedData.totalRows);
        }
    }
}
