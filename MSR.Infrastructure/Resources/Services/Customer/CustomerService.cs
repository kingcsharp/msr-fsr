using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == id);

            if(customer is null)
            {
                throw new DomainException($"{nameof(Customer)} does not exist with {nameof(id)}: {id}");
            }

            return _mapper.Map<Customer>(customer);
        }

        public async Task<IEnumerable<Customer>> GetCustomers(GetMultipleCustomers command)
        {
            var customerList = new List<Customer>();
            var customers = _unitOfWork.Customers.Query();

            if (!string.IsNullOrWhiteSpace(command.Name))
            {
                customers = customers.Where(i => i.Name == command.Name);
            }
            if (!string.IsNullOrWhiteSpace(command.Address))
            {
                customers = customers.Where(i => i.Address == command.Address);
            }
            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                customers = customers.Where(i => i.Phone == command.Phone);
            }
            if (command.PrimaryContactUserId.HasValue)
            {
                customers = customers.Where(i => i.PrimaryContactUserId == command.PrimaryContactUserId.Value);
            }
            if (command.SecondaryContactUserId.HasValue)
            {
                customers = customers.Where(i => i.SecondaryContactUserId == command.SecondaryContactUserId.Value);
            }
            if (command.LocationId.HasValue)
            {
                customers = customers.Where(i => i.LocationId == command.LocationId.Value);
            }
            if (command.IsActive.HasValue)
            {
                customers = customers.Where(i => i.IsActive == command.IsActive.Value);
            }

            foreach(var customer in customers.ToList())
            {
                customerList.Add(_mapper.Map<Customer>(customer));
            }

            return customerList.AsEnumerable();
        }


    }
}
