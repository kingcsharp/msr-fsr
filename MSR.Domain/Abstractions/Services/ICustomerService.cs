using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface ICustomerService
    {
        Task<CustomerModel> GetCustomerAsync(int id);
        Task<IEnumerable<CustomerModel>> GetCustomersAsync(GetMultipleCustomers command);
        Task<CustomerModel> CreateCustomerAsync(CreateCustomer command, bool import = false);
        Task<CustomerModel> UpdateCustomerAsync(UpdateCustomer command, bool import = false);
        Task<CustomerModel> DeleteCustomerAsync(int Id);
        Task<IEnumerable<CustomerModel>> ImportCustomers(string csvData);
        Task<int> GetTotalCustomerRows(GetMultipleCustomers command);
    }
}
