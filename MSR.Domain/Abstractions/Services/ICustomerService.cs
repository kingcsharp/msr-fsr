using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerAsync(int id);
        Task<IEnumerable<Customer>> GetCustomersAsync(GetMultipleCustomers command);
        Task<Customer> CreateCustomerAsync(CreateCustomer command);
        Task<Customer> UpdateCustomerAsync(UpdateCustomer command);
        Task<Customer> DeleteCustomerAsync(int Id);
        Task<IEnumerable<Customer>> ImportCustomers(string csvData);
    }
}
