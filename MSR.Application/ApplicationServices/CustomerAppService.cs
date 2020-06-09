using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class CustomerAppService :
        ICommandHandler<GetCustomer>,
        ICommandHandler<GetMultipleCustomers>
    {

        private readonly ICustomerService _customerService;

        public CustomerAppService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ICommandResponse> HandleAsync(GetCustomer command, CancellationToken cancellationToken = default)
        {
            var ret = await _customerService.GetCustomer(command.Id);
            return new CommandResponse<Customer>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetMultipleCustomers command, CancellationToken cancellationToken = default)
        {
            var ret = await _customerService.GetCustomers(command);
            return new CommandResponse<IEnumerable<Customer>>(ret);
        }
    }
}
