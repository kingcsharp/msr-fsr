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
        ICommandHandler<GetMultipleCustomers>,
        ICommandHandler<CreateCustomer>,
        ICommandHandler<UpdateCustomer>,
        ICommandHandler<DeactivateCustomer>
    {

        private readonly ICustomerService _customerService;

        public CustomerAppService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ICommandResponse> HandleAsync(GetCustomer command, CancellationToken cancellationToken = default)
        {
            var ret = await _customerService.GetCustomerAsync(command.Id);
            return new CommandResponse<Customer>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetMultipleCustomers command, CancellationToken cancellationToken = default)
        {
            var ret = await _customerService.GetCustomersAsync(command);
            return new CommandResponse<IEnumerable<Customer>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateCustomer command, CancellationToken cancellationToken = default)
        {
            var ret = await _customerService.CreateCustomerAsync(command);
            return new CommandResponse<Customer>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeactivateCustomer command, CancellationToken cancellationToken = default)
        {
            await _customerService.DeleteCustomerAsync(command.CustomerId);
            return new CommandResponse();
        }

        public async Task<ICommandResponse> HandleAsync(UpdateCustomer command, CancellationToken cancellationToken = default)
        {
            var ret = await _customerService.UpdateCustomerAsync(command);
            return new CommandResponse<Customer>(ret);
        }
    }
}
