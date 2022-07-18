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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MSR.Domain.Models;

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

        public async Task<ICollection<Domain.Models.PurchaseModel>> CreatePurchaseAsync(CreatePurchase command)
        {
            List<Domain.Models.PurchaseModel> ret = new List<PurchaseModel>();
            EntityEntry<Purchase> created;
            Purchase purchase;

            if (CurrentUser.HasPrivilege(EnumMenuItem.Purchases, EnumPrivilege.CanCreate))
            {
                if (command.GroupLines)
                {
                    purchase = _mapper.Map<Purchase>(command.PurchaseRequests.First());
                    purchase.SerialNumber = command.PurchaseRequests.First().SerialNumbers.FirstOrDefault();
                    created = _unitOfWork.Purchases.Add(purchase);
                    await _unitOfWork.SaveChangesAsync();
                    purchase.CustomerPurchaseNumber = purchase.Id.ToString();

                    var workOrderCreateEvent = new WorkOrderCreateEvent
                    {
                        HasNCR = false,
                        LocationId = command.PurchaseRequests.First().LocationId,
                        PurchaseId = purchase.Id,
                        PurchaseOrderId = command.PurchaseRequests.First().PurchaseOrderId,
                        ScheduledEndDate = command.PurchaseRequests.First().DueDate,
                        ScheduledStartDate = DateTime.UtcNow
                    };

                    foreach (var item in command.PurchaseRequests)
                    {
                        var product = await _unitOfWork.PurchaseOrderProducts.FirstOrDefaultAsync(false,i => i.Id == item.PurchaseOrderProductId);
                        var purchaseProductMap = new PurchaseProductMap()
                        {
                            PurchaseId = purchase.Id,
                            PurchaseOrderProductId = item.PurchaseOrderProductId,
                            Qty = item.Qty
                        };

                        _unitOfWork.PurchaseProductMaps.Add(purchaseProductMap);

                        workOrderCreateEvent.WorkOrderProducts.Add(new WorkOrderProduct(product.Id, item.SerializeIndividually, item.SerialNumbers, item.CustomerLineNumbers, item.Qty, item.PurchasePrice));
                    }

                    await _unitOfWork.LogApprovalTransaction(purchase, purchase.Id);

                    var sqsMessageEnvelope = new MessageEnvelope(workOrderCreateEvent.GetType().Name, workOrderCreateEvent, await _accountService.GetJWTTokenAsync());
                    await _bus.SendMessage(sqsMessageEnvelope);

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

                    var purchaseModel = _mapper.Map<PurchaseModel>(purchase);
                    purchaseModel.SerializeIndividually = command.PurchaseRequests.First().SerializeIndividually;
                    ret.Add(purchaseModel);
                    
                }
                else 
                { 
                    foreach(var item in command.PurchaseRequests)
                    {
                        purchase = _mapper.Map<Purchase>(item);
                        created = _unitOfWork.Purchases.Add(purchase);
                        await _unitOfWork.SaveChangesAsync();

                        purchase.CustomerPurchaseNumber = purchase.Id.ToString();
                        var purchaseProductMap = new PurchaseProductMap()
                        {
                            PurchaseId = purchase.Id,
                            PurchaseOrderProductId = item.PurchaseOrderProductId,
                            Qty = item.Qty
                        };

                        _unitOfWork.PurchaseProductMaps.Add(purchaseProductMap);

                        await _unitOfWork.LogApprovalTransaction(purchase, purchase.Id);
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

                        var purchaseModel = _mapper.Map<Domain.Models.PurchaseModel>(purchase);
                        purchaseModel.SerializeIndividually = item.SerializeIndividually;
                        ret.Add(purchaseModel);

                        var workOrderCreateEvent = new WorkOrderCreateEvent
                        {
                            HasNCR = false,
                            LocationId = command.PurchaseRequests.First().LocationId,
                            PurchaseId = purchase.Id,
                            PurchaseOrderId = command.PurchaseRequests.First().PurchaseOrderId,
                            ScheduledEndDate = command.PurchaseRequests.First().DueDate,
                            ScheduledStartDate = DateTime.UtcNow
                        };

                        var product = await _unitOfWork.PurchaseOrderProducts.FirstOrDefaultAsync(false, i => i.Id == item.PurchaseOrderProductId);
                        workOrderCreateEvent.WorkOrderProducts.Add(new WorkOrderProduct(product.Id, item.SerializeIndividually, item.SerialNumbers, item.CustomerLineNumbers, item.Qty, item.PurchasePrice));
                        var sqsMessageEnvelope = new MessageEnvelope(workOrderCreateEvent.GetType().Name, workOrderCreateEvent, await _accountService.GetJWTTokenAsync());
                        await _bus.SendMessage(sqsMessageEnvelope);
                    }
                }

               

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
