using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MSR.Infrastructure.Resources.Queries;

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
            return new CommandResponse<CustomerModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetMultipleCustomers command, CancellationToken cancellationToken = default)
        {
            var customerModels = await _customerService.GetCustomersAsync(command);
            var totalRows = customerModels.AsQueryable().CreateCustomerQuery(command, true).Count();
            customerModels = customerModels.AsQueryable().CreateCustomerQuery(command).ToList();
            return new PagingCommandResponse<IEnumerable<CustomerModel>>(customerModels, totalRows, command.Term, command.PageNumber, command.PageSize, command.SortAscending);
        }

        public async Task<ICommandResponse> HandleAsync(CreateCustomer command, CancellationToken cancellationToken = default)
        {
            var ret = await _customerService.CreateCustomerAsync(command);
            return new CommandResponse<CustomerModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeactivateCustomer command, CancellationToken cancellationToken = default)
        {
            var ret = await _customerService.DeleteCustomerAsync(command.CustomerId);
            return new CommandResponse<CustomerModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateCustomer command, CancellationToken cancellationToken = default)
        {
            var ret = await _customerService.UpdateCustomerAsync(command);
            return new CommandResponse<CustomerModel>(ret);
        }
    }
}
