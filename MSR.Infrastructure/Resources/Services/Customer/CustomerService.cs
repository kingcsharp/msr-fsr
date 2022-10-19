using System;
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
using MSR.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Reflection;
using MSR.Domain.Commanding;
using System.Diagnostics;
using System.Linq.Expressions;
using MSR.Infrastructure.Resources.Queries;

namespace MSR.Infrastructure.Resources.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger _logger;
        private readonly IMessageHubClient _messageHub;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CustomerService> logger, IMessageHubClient messageHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _messageHub = messageHub;
        }

        public async Task<CustomerModel> CreateCustomerAsync(CreateCustomer command, bool import = false)
        {
            CustomerModel retCustomer;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.CustomerApproval) || import)
            {
                var customer = _mapper.Map<Customer>(command);
                //Because automapper is stupid.
                if (customer.LocationId == 0)
                {
                    customer.LocationId = null;
                }
                if (customer.PrimaryContactUserId == 0)
                {
                    customer.PrimaryContactUserId = null;
                }
                if (customer.SecondaryContactUserId == 0)
                {
                    customer.SecondaryContactUserId = null;
                }

                customer.IsActive = true;
                await _unitOfWork.Customers.AddAsync(customer);
                await _unitOfWork.LogApprovalTransaction(customer, customer.Id);

                retCustomer = _mapper.Map<CustomerModel>(customer);
            }
            else
            {
                var customerApproval = _mapper.Map<CustomerApproval>(command);
                customerApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(customerApproval);
                customerApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(customerApproval.Workflow?.Id ?? 0);
                customerApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                customerApproval.IsActive = true;
                if (customerApproval.LocationId == 0)
                {
                    customerApproval.LocationId = null;
                }

                await _unitOfWork.CustomerApprovals.AddAsync(customerApproval);
                await _unitOfWork.SaveChangesAsync();
                retCustomer = _mapper.Map<CustomerModel>(customerApproval);
                _messageHub.SendApprovalNotification(EnumApprovalTables.CustomerApproval);
            }
            return retCustomer;
        }

        public async Task<CustomerModel> UpdateCustomerAsync(UpdateCustomer command, bool import = false)
        {
            CustomerModel retCustomer = null;

            var curCustomer = await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == command.CustomerId);

            if (curCustomer is null)
            {
                throw new DomainException($"{nameof(CustomerModel)} not found with ID: {command.CustomerId}");
            }

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.CustomerApproval) || import)
            {
                var customer = _mapper.Map(command, curCustomer);
                customer.PrimaryContactUserId = command.PrimaryContactUserId;
                customer.SecondaryContactUserId = command.SecondaryContactUserId;
                customer.LocationId = command.LocationId;
                _unitOfWork.Customers.Update(curCustomer);
                await _unitOfWork.LogApprovalTransaction(curCustomer, curCustomer.Id);

                retCustomer = _mapper.Map<CustomerModel>(curCustomer);
            }
            else
            {
                var customerApproval = _mapper.Map<CustomerApproval>(curCustomer);
                _mapper.Map(command, customerApproval);

                customerApproval.CustomerId = curCustomer.Id;
                customerApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(customerApproval);
                customerApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(customerApproval.Workflow?.Id ?? 0);
                customerApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                var ret = await _unitOfWork.CustomerApprovals.AddAsync(customerApproval);
                await _unitOfWork.SaveChangesAsync();

                retCustomer = _mapper.Map<CustomerModel>(customerApproval);
                _messageHub.SendApprovalNotification(EnumApprovalTables.CustomerApproval);
            }


            return retCustomer;
        }

        public async Task<CustomerModel> DeleteCustomerAsync(int Id)
        {
            var customer = _unitOfWork.Customers.FirstOrDefault(false, i => i.Id == Id);

            if (customer is null)
            {
                throw new DomainException($"{nameof(CustomerModel)} not found with ID: {Id}");
            }

            CustomerModel retCustomer = null;

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
                retCustomer = _mapper.Map<CustomerModel>(customer);
            }
            else
            {
                var customerApproval = _mapper.Map<CustomerApproval>(customer);
                customerApproval.IsActive = false;
                customerApproval.CustomerId = customer.Id;
                customerApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(customerApproval);
                customerApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(customerApproval.Workflow?.Id ?? 0);
                customerApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
                await _unitOfWork.CustomerApprovals.AddAsync(customerApproval);
                await _unitOfWork.SaveChangesAsync();

                retCustomer = _mapper.Map<CustomerModel>(customerApproval);
                _messageHub.SendApprovalNotification(EnumApprovalTables.CustomerApproval);
            }
            return retCustomer;
        }

        public async Task<CustomerModel> GetCustomerByNameAsync(string name)
        {
            var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Name.Contains(name.Trim().ToLower()));
            if (customer is null)
            {
                throw new DomainException($"{nameof(CustomerModel)} does not exist with {nameof(name)}: {name}");
            }
            var retCustomer = _mapper.Map<CustomerModel>(customer);

            return retCustomer;
        }

        public async Task<CustomerModel> GetCustomerAsync(int id)
        {
            var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == id);

            if (customer is null)
            {
                throw new DomainException($"{nameof(CustomerModel)} does not exist with {nameof(id)}: {id}");
            }

            var customerApproval = await _unitOfWork.CustomerApprovals.FirstOrDefaultAsync(false, i => i.CustomerId == id);

            var retCustomer = _mapper.Map<CustomerModel>(customer);
            if (customerApproval != null)
            {
                _mapper.Map(customerApproval, retCustomer);
            }

            return retCustomer;
        }

        public async Task<List<CustomerModel>> GetCustomersAsync(GetMultipleCustomers command)
        {
            var customerModels = new List<CustomerModel>();


            var customerEntities = await _unitOfWork.Customers.Query().Include(i => i.Location).Include(i => i.PrimaryContactUser).Include(i => i.SecondaryContactUser).ToListAsync();
            var customerEntityIds = customerEntities.Select(s => s.Id).ToList();

            var customerApprovalEntities = await _unitOfWork.CustomerApprovals.Query().Include(i => i.Status).Where(i => customerEntityIds.Contains(i.CustomerId.Value)).ToListAsync();

            foreach (var customerEntity in customerEntities.ToList())
            {
                var customerApprovalEntity = customerApprovalEntities.FirstOrDefault(i => i.CustomerId == customerEntity.Id);

                var customerModel = _mapper.Map<CustomerModel>(customerEntity);
                if (customerApprovalEntity != null && customerApprovalEntity.Status != null)
                {
                    _mapper.Map(customerApprovalEntity, customerModel);
                    customerModel.Status = customerApprovalEntity.Status.Name;
                }
                else
                {
                    customerModel.Status = "Approved";
                }
                customerModels.Add(customerModel);
            }

            return customerModels.OrderBy(i => i.Name).ToList();
        }

        public async Task<IEnumerable<CustomerModel>> ImportCustomers(string csvData)
        {
            var records = CSVHelper.ParseRecords<CustomerImportItem>(csvData);
            var customerModels = new List<CustomerModel>();
            var customerIds = await _unitOfWork.Customers.Query().Select(i => i.Id).ToListAsync();

            if (!CurrentUser.HasPrivilege(EnumMenuItem.CustomersDepartments, EnumPrivilege.CanApprove))
            {
                throw new DomainException("Permission denied for import", DomainError.BadRequest);
            }

            foreach (var record in records)
            {
                try
                {
                    if (record.Id.HasValue && record.Id.Value > 0 && customerIds.Contains(record.Id.Value))
                    {
                        var customerModel = await UpdateCustomerAsync(_mapper.Map<UpdateCustomer>(record), true);
                        customerModels.Add(customerModel);
                    }
                    else
                    {
                        var customerModel = await CreateCustomerAsync(_mapper.Map<CreateCustomer>(record), true);
                        customerModels.Add(customerModel);
                    }
                }
                catch (Exception exception)
                {
                    //If we get an error on a single import dump it and keep going. 
                    _logger.LogError(exception, exception.Message);
                }
            }

            return customerModels;
        }

    }
}
