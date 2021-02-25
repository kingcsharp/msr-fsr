using MSR.Domain.Models;
using MSR.Domain.Models.Paging;
using System.Threading.Tasks;

namespace MSR.Application.Abstractions
{
    public interface ICustomerViewService
    {
        Task<ApiPagingModel<CustomerModel>> GetCustomers(ApiPagingModel<CustomerModel> paging, int? Id, string name, string address, string phone, int? primaryContactUserId, int? secondaryContactUserId, int? locationId, bool? isActive);
    }
}
