using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Application.Abstractions;
using MSR.Domain.Models;
using MSR.Domain.Models.Paging;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Application.ViewServices
{
    public class CustomerViewService : ICustomerViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerViewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(ICollection<CustomerModel> data, int totalRows)> GetCustomers(int skip, int take, int? Id, string name, string address, string phone, int? primaryContactUserId, int? secondaryContactUserId, int? locationId, bool? isActive)
        {
            var customerList = new List<CustomerModel>();
            var customers = _unitOfWork.Query<Customer>().GetCustomersView(skip, take, Id, name, address, phone, primaryContactUserId, secondaryContactUserId, locationId, isActive);

            foreach (var customer in customers.data)
            {
                var customerApproval = await _unitOfWork.CustomerApprovals.Query().Include(i => i.Status).FirstOrDefaultAsync(i => i.CustomerId == customer.Id);

                var retCustomer = _mapper.Map<CustomerModel>(customer);
                if (customerApproval != null && customerApproval.Status != null)
                {
                    _mapper.Map(customerApproval, retCustomer);
                    retCustomer.Status = customerApproval.Status.Name;
                }
                customerList.Add(retCustomer);
            }

            return (customerList, customers.totalRows);
        }
    }
}
