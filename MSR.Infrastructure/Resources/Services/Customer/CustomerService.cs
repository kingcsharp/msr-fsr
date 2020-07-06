using AutoMapper;
using Microsoft.IdentityModel.Logging;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
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
            _mapper = mapper;
        }

        public async Task<Domain.Models.Customer> CreateCustomerAsync(CreateCustomer command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();

            Domain.Models.Customer retCustomer = null;

            if (user.CanApprove(EnumMenuItem.CustomersDepartments))
            {
                var customer = _mapper.Map<EntityFramework.Entities.Customer>(command);
                await _unitOfWork.Customers.AddAsync(customer);
                retCustomer = _mapper.Map<Domain.Models.Customer>(customer);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.LogApprovalTransaction(customer, customer.Id);
            }
            else
            {
                var customerApproval = _mapper.Map<CustomerApproval>(command);
                var ret = await _unitOfWork.CustomerApprovals.AddAsync(customerApproval);
                retCustomer = _mapper.Map<Domain.Models.Customer>(customerApproval);
                await _unitOfWork.SaveChangesAsync();

            }



            return retCustomer;
        }

        public async Task<Domain.Models.Customer> UpdateCustomerAsync(UpdateCustomer command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();

            Domain.Models.Customer retCustomer = null;

            var curCustomer = await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == command.CustomerId);

            if (curCustomer is null)
            {
                throw new DomainException($"{nameof(Domain.Models.Customer)} not found with ID: {command.CustomerId}");
            }

            if (user.CanApprove(EnumMenuItem.CustomersDepartments))
            {
                UpdateCustomerRecord(curCustomer, command);
                _unitOfWork.Customers.Update(curCustomer);
                retCustomer = _mapper.Map<Domain.Models.Customer>(curCustomer);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.LogApprovalTransaction(curCustomer, curCustomer.Id);
            }
            else
            {
                var customerApproval = _mapper.Map<CustomerApproval>(command);
                customerApproval.CustomerId = curCustomer.Id;
                customerApproval.Customer = curCustomer;

                var ret = await _unitOfWork.CustomerApprovals.AddAsync(customerApproval);
                retCustomer = _mapper.Map<Domain.Models.Customer>(ret.Entity);
                await _unitOfWork.SaveChangesAsync();
            }


            return retCustomer;
        }

        public async Task DeleteCustomerAsync(int Id)
        {
            var curUser = await _unitOfWork.GetLoggedInUserAsync();

            var customer = _unitOfWork.Customers.FirstOrDefault(false, i => i.Id == Id);

            if (customer is null)
            {
                throw new DomainException($"{nameof(Domain.Models.Customer)} not found with ID: {Id}");
            }

            customer.IsActive = false;

            if (curUser.CanApprove(EnumMenuItem.CustomersDepartments))
            {
                //TODO: Add Products
                _unitOfWork.Customers.Update(customer);
                var users = _unitOfWork.Users.Query().Where(i => i.CustomerId == customer.Id).ToList();
                foreach (var user in users)
                {
                    user.IsActive = false;
                    _unitOfWork.Users.Update(user);
                }
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.LogApprovalTransaction(customer, customer.Id);
            }
            else
            {
                _unitOfWork.CustomerApprovals.Add(new CustomerApproval()
                {
                    Address = customer.Address,
                    Customer = customer,
                    CustomerId = customer.Id,
                    LocationId = customer.LocationId,
                    Location = customer.Location,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    IsActive = false
                });
                await _unitOfWork.SaveChangesAsync();
            }

        }

        public async Task<Domain.Models.Customer> GetCustomerAsync(int id)
        {
            var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == id);

            if (customer is null)
            {
                throw new DomainException($"{nameof(Domain.Models.Customer)} does not exist with {nameof(id)}: {id}");
            }

            return _mapper.Map<Domain.Models.Customer>(customer);
        }

        public async Task<IEnumerable<Domain.Models.Customer>> GetCustomersAsync(GetMultipleCustomers command)
        {
            var customerList = new List<Domain.Models.Customer>();
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

            foreach (var customer in customers.ToList())
            {
                customerList.Add(_mapper.Map<Domain.Models.Customer>(customer));
            }

            return customerList.AsEnumerable();
        }
        private void UpdateCustomerRecord(EntityFramework.Entities.Customer curCustomer, UpdateCustomer command)
        {
            curCustomer.Address = command.Address ?? curCustomer.Address;
            curCustomer.Name = command.Name ?? curCustomer.Name;
            curCustomer.Phone = command.Phone ?? curCustomer.Phone;
            curCustomer.LocationId = command.LocationId;
            curCustomer.PrimaryContactUserId = command.PrimaryContactUserId.HasValue ? command.PrimaryContactUserId : curCustomer.PrimaryContactUserId.Value;
            curCustomer.SecondaryContactUserId = command.SecondaryContactUserId.HasValue ? command.SecondaryContactUserId : curCustomer.SecondaryContactUserId.Value;
        }
    }
}
