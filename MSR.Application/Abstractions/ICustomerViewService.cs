using MSR.Domain.Models;
using MSR.Domain.Models.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Application.Abstractions
{
    public interface ICustomerViewService
    {
        Task<(ICollection<CustomerModel> data, int totalRows)> GetCustomers(int skip, int take, int? Id, string name, string address, string phone, int? primaryContactUserId, int? secondaryContactUserId, int? locationId, bool? isActive);
    }
}
