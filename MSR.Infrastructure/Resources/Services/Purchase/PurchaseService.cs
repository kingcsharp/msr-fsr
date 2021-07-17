using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Events;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.SQSEventing.Abstractions;
using MSR.Domain.SQSEventing.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using MSR.Infrastructure.Resources.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.PurchaseOrder
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISendSQSMessages _bus;
        private readonly IAccountService _accountService;

        public PurchaseService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAccountService accountService,
            ISendSQSMessages bus)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bus = bus;
            _accountService = accountService;
        }

        public async Task<ICollection<Domain.Models.PurchaseModel>> GetPurchasesAsync(GetPurchases command)
        {
            List<Purchase> purchaseEntities = await _unitOfWork.Purchases.Query().CreatePurchaseQuery(command).ToListAsync();

            if (purchaseEntities.Count == 0 && command.Id.HasValue)
            {
                throw new DomainException($"procedure ID {command.Id.Value} not found", DomainError.NotFound);
            }

            var result = purchaseEntities.Select(x => _mapper.Map<Domain.Models.PurchaseModel>(x)).ToList();
            return result;
        }

        public async Task<Domain.Models.PurchaseModel> CreatePurchaseAsync(CreatePurchase command)
        {
            Domain.Models.PurchaseModel ret;

            if (command.StatusId == 0) {
                throw new DomainException("Invalid Status of 0", DomainError.BadRequest);
            }

            if (CurrentUser.HasPrivilege(EnumMenuItem.Purchases, EnumPrivilege.CanCreate)) {
                var purchase = _mapper.Map<Purchase>(command);
                var created = _unitOfWork.Purchases.Add(purchase);

                // Call SaveChangesAsync to generate the new ID
                await _unitOfWork.SaveChangesAsync();

                // CustomerPurchaseNumber is the same as the DB id
                // In answer 2 it was a sequential integer based on the object
                // table.  In 3, we just use the purchase Id as a string.
                purchase.CustomerPurchaseNumber = purchase.Id.ToString();

                // log the transaction
                await _unitOfWork.LogApprovalTransaction(purchase, purchase.Id);

                // load required navigation fields
                await created.Context.Entry(purchase)
                    .Reference(x => x.Status).LoadAsync();
                await created.Context.Entry(purchase)
                    .Reference(x => x.Location).LoadAsync();
                await created.Context.Entry(purchase)
                    .Reference(x => x.PurchaseOrder).LoadAsync();
                await created.Context.Entry(purchase)
                    .Reference(x => x.PurchaseOrderProduct).LoadAsync();
                await created.Context.Entry(purchase.PurchaseOrderProduct)
                    .Reference(x => x.Product).LoadAsync();

                ret = _mapper.Map<Domain.Models.PurchaseModel>(purchase);
                ret.SerializeIndividually = command.SerializeIndividually;

                var workOrderCreateEvent = new WorkOrderCreateEvent
                {
                    SerialNumbers = command.SerialNumbers,
                    CustomerLineNumbers = command.CustomerLineNumbers,
                    Qty = ret.Qty,
                    Price = ret.PurchasePrice,
                    LocationId = ret.LocationId,
                    PurchaseId = ret.Id,
                    ScheduledEndDate = ret.DueDate,
                    ScheduledStartDate = DateTime.UtcNow,
                    ProductId = ret.PurchaseOrderProduct?.ProductId,
                    SerializeIndividually = ret.SerializeIndividually,
                    HasNCR = false,
                    PurchaseOrderId = ret.PurchaseOrderId
                };

                var sqsMessageEnvelope = new MessageEnvelope(workOrderCreateEvent.GetType().Name,workOrderCreateEvent,await _accountService.GetJWTTokenAsync());
                await _bus.SendMessage(sqsMessageEnvelope);

            }
            else
            {
                throw new DomainException($"Permission denied on {nameof(Purchase)} for user {CurrentUser.GetId()}");
            }

            return ret;
        }

        public async Task<int> GetPurchaseTotalRows(GetPurchases command)
        {
            var totalRows = await _unitOfWork.Purchases.Query().CreatePurchaseQuery(command, true).CountAsync();
            return totalRows;
        }
    }
}
