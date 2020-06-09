using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomer(int id);
        Task<IEnumerable<Customer>> GetCustomers(GetMultipleCustomers command);
    }
}
