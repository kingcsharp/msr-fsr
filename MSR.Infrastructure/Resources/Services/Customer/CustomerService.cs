using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Commanding.Abstractions;

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
            Domain.Models.Customer retCustomer;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.CustomerApproval))
            {
                var customer = _mapper.Map<EntityFramework.Entities.Customer>(command);
                //Because automapper is stupid.
                if(customer.LocationId == 0)
                {
                    customer.LocationId = null;
                }
                if(customer.PrimaryContactUserId == 0)
                {
                    customer.PrimaryContactUserId = null;
                }
                if(customer.SecondaryContactUserId == 0)
                {
                    customer.SecondaryContactUserId = null;
                }

                await _unitOfWork.Customers.AddAsync(customer);
                await _unitOfWork.LogApprovalTransaction(customer, customer.Id);
                
                retCustomer = _mapper.Map<Domain.Models.Customer>(customer);
            }
            else
            {
                var customerApproval = _mapper.Map<CustomerApproval>(command);
                customerApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(customerApproval);
                customerApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(customerApproval.Workflow?.Id ?? 0);
                customerApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatus.Pending);
                
                if (customerApproval.LocationId == 0)
                {
                    customerApproval.LocationId = null;
                }

                await _unitOfWork.CustomerApprovals.AddAsync(customerApproval);
                await _unitOfWork.SaveChangesAsync();
                retCustomer = _mapper.Map<Domain.Models.Customer>(customerApproval);
            }

            //We are doing this because Automapper won't let us have nulls for some reason. 
            if (retCustomer.Location.Id == 0)
            {
                retCustomer.Location = null;
            }
            if (retCustomer.PrimaryContactUser.Id == 0)
            {
                retCustomer.PrimaryContactUser = null;
            }
            if (retCustomer.SecondaryContactUser.Id == 0)
            {
                retCustomer.SecondaryContactUser = null;
            }
            return retCustomer;
        }

        public async Task<Domain.Models.Customer> UpdateCustomerAsync(UpdateCustomer command)
        {
            Domain.Models.Customer retCustomer = null;

            var curCustomer = await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == command.CustomerId);

            if (curCustomer is null)
            {
                throw new DomainException($"{nameof(Domain.Models.Customer)} not found with ID: {command.CustomerId}");
            }

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.CustomerApproval))
            {
                var customer = _mapper.Map(command, curCustomer);
                _unitOfWork.Customers.Update(curCustomer);
                await _unitOfWork.LogApprovalTransaction(curCustomer, curCustomer.Id);

                retCustomer = _mapper.Map<Domain.Models.Customer>(curCustomer);
            }
            else
            {
                var customerApproval = _mapper.Map<CustomerApproval>(curCustomer);
                _mapper.Map(command, customerApproval);

                customerApproval.CustomerId = curCustomer.Id;
                customerApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(customerApproval);
                customerApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(customerApproval.Workflow?.Id ?? 0);
                customerApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatus.Pending);

                var ret = await _unitOfWork.CustomerApprovals.AddAsync(customerApproval);
                await _unitOfWork.SaveChangesAsync();

                retCustomer = _mapper.Map<Domain.Models.Customer>(customerApproval);
            }


            return retCustomer;
        }

        public async Task<Domain.Models.Customer> DeleteCustomerAsync(int Id)
        {
            var customer = _unitOfWork.Customers.FirstOrDefault(false, i => i.Id == Id);

            if (customer is null)
            {
                throw new DomainException($"{nameof(Domain.Models.Customer)} not found with ID: {Id}");
            }

            Domain.Models.Customer retCustomer = null;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.CustomerApproval))
            {
                customer.IsActive = false;
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
                retCustomer = _mapper.Map<Domain.Models.Customer>(customer);
            }
            else
            {
                var customerApproval = _mapper.Map<CustomerApproval>(customer);
                customerApproval.IsActive = false;
                customerApproval.CustomerId = customer.Id;
                customerApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(customerApproval);
                customerApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(customerApproval.Workflow?.Id ?? 0);
                customerApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatus.Pending);
                await _unitOfWork.CustomerApprovals.AddAsync(customerApproval);
                await _unitOfWork.SaveChangesAsync();

                retCustomer = _mapper.Map<Domain.Models.Customer>(customerApproval);
            }
            return retCustomer;
        }

        public async Task<Domain.Models.Customer> GetCustomerAsync(int id)
        {
            var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == id);

            if (customer is null)
            {
                throw new DomainException($"{nameof(Domain.Models.Customer)} does not exist with {nameof(id)}: {id}");
            }

            var customerApproval = await _unitOfWork.CustomerApprovals.FirstOrDefaultAsync(false, i => i.CustomerId == id);

            var retCustomer = _mapper.Map<Domain.Models.Customer>(customer);
            if (customerApproval != null)
            {
                _mapper.Map(customerApproval, retCustomer);
            }

            return retCustomer;
        }

        public async Task<IEnumerable<Domain.Models.Customer>> GetCustomersAsync(GetMultipleCustomers command)
        {
            var customerList = new List<Domain.Models.Customer>();
            var customers = _unitOfWork.Customers.Query().Include(i => i.Location).Include(i => i.PrimaryContactUser).Include(i => i.SecondaryContactUser).Where(i => i.IsActive);

            if (command.Id != null)
            {
                customers = customers.Where(i => i.Id == command.Id);
            }
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
                var customerApproval = await _unitOfWork.CustomerApprovals.Query().Include(i => i.Status).FirstOrDefaultAsync(i => i.CustomerId == customer.Id);

                var retCustomer = _mapper.Map<Domain.Models.Customer>(customer);
                if (customerApproval != null && customerApproval.Status != null)
                {
                    _mapper.Map(customerApproval, retCustomer);
                    retCustomer.Status = customerApproval.Status.Name;
                }
                customerList.Add(retCustomer);
            }

            return customerList.AsEnumerable();
        }

        public async Task<IEnumerable<Domain.Models.Customer>> ImportCustomers(string csvData)
        {
            var records = CSVHelper.ParseRecords<CustomerImportItem>(csvData);
            var customers = new List<Domain.Models.Customer>();

            if (!CurrentUser.HasPrivilege(EnumMenuItem.CustomersDepartments, EnumPrivilege.CanApprove))
            {
                throw new DomainException("Permission denied for import", DomainError.BadRequest);
            }

            foreach (var record in records)
            {
                try
                {
                    if (record.Id.HasValue && record.Id.Value > 0)
                    {
                        var ret = await UpdateCustomerAsync(_mapper.Map<UpdateCustomer>(record));
                        customers.Add(ret);
                    }
                    else
                    {
                        var ret = await CreateCustomerAsync(_mapper.Map<CreateCustomer>(record));
                        customers.Add(ret);
                    }
                }
                catch
                {
                    //If we get an error on a single import dump it and keep going. 
                }
            }

            return customers;
        }

    }
}
