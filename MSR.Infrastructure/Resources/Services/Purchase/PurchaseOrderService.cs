using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace MSR.Infrastructure.Resources.Services.PurchaseOrder
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseOrderView>> GetPurchaseOrderAsync(GetPurchaseOrder command)
        {
            var purchaseOrderQuery = _unitOfWork.PurchaseOrders.Query();
            if (command.Id.HasValue)
            {
                purchaseOrderQuery = purchaseOrderQuery.Where(i => i.Id == command.Id);
            }

            var purchaseOrderEntities = await purchaseOrderQuery
                .Include(i => i.Status)
                .Include(i => i.PurchaseOrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(pt => pt.Part)
                .Include(i => i.PurchaseOrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(pt => pt.Procedure)
                .ToListAsync();

            var purchaseOrderViews = new List<PurchaseOrderView>();

            foreach (var purchaseOrderEntity in purchaseOrderEntities)
            {
                var purchaseOrderView = _mapper.Map<PurchaseOrderView>(purchaseOrderEntity);
                purchaseOrderView.Products = purchaseOrderEntity.PurchaseOrderProducts
                    .Select(x => _mapper.Map<PurchaseOrderProductView>(x))
                    .ToList();
                purchaseOrderViews.Add(purchaseOrderView);
            }

            var purchaseOrderIds = purchaseOrderViews.Select(i => i.Id).ToList();
            var purchaseOrderCustomerIds = purchaseOrderViews.Select(i => i.CustomerId).ToList();

            var customerEntities = await _unitOfWork.Customers.Query()
                .Where(i => purchaseOrderCustomerIds.Contains(i.Id))
                .Select(i => new { i.Id, i.Name })
                .ToDictionaryAsync(i => i.Id, i => i.Name);

            var purchaseEntities = await _unitOfWork.Purchases.Query()
                .Where(i => purchaseOrderIds.Contains(i.PurchaseOrderId))
                .ToListAsync();

            var purchaseIds = purchaseEntities.Select(i => i.Id).ToList();

            var workOrderEntities = await _unitOfWork.WorkOrders.Query()
                .Where(i => i.ActualEndDate != null && purchaseIds.Contains(i.PurchaseId))
                .Include(i => i.Purchase)
                .ToListAsync();

            var invoicedWorkOrderIds = await _unitOfWork.InvoiceItems.Query()
                .Where(i => purchaseOrderIds.Contains(i.PurchaseOrderId))
                .Select(i => i.WorkOrderId)
                .Distinct()
                .ToListAsync();

            foreach (var purchaseOrderView in purchaseOrderViews)
            {
                customerEntities.TryGetValue(purchaseOrderView.CustomerId, out var name);
                if (name != null)
                {
                    purchaseOrderView.CustomerName = name;
                }
                purchaseOrderView.IsDeletable = purchaseEntities.All(i => i.PurchaseOrderId != purchaseOrderView.Id);
                purchaseOrderView.InvoicedBalance = 0;
                purchaseOrderView.Balance = 0;
                purchaseOrderView.UninvoicedBalance = 0;

                var purchaseOrderWorkOrderEntities = workOrderEntities
                    .Where(i => i.Purchase.PurchaseOrderId == purchaseOrderView.Id)
                    .ToList();

                foreach (var purchaseOrderWorkOrderEntity in purchaseOrderWorkOrderEntities)
                {
                    purchaseOrderView.Balance += purchaseOrderWorkOrderEntity.Price;
                    if (invoicedWorkOrderIds.Contains(purchaseOrderWorkOrderEntity.Id))
                    {
                        purchaseOrderView.InvoicedBalance += purchaseOrderWorkOrderEntity.Price;
                    }
                    else
                    {
                        purchaseOrderView.UninvoicedBalance += purchaseOrderWorkOrderEntity.Price;
                    }
                }

                purchaseOrderView.UnusedAmount = purchaseOrderView.TotalPurchaseLimit - purchaseOrderView.Balance;
            }

            return purchaseOrderViews;
        }

        public async Task<PurchaseOrderView> CreatePurchaseOrderAsync(CreatePurchaseOrder command)
        {
            //First, Check if the user has CanApprove or not.
            if (CurrentUser.CanApproveActivity(EnumApprovalTables.PurchaseOrderApproval))
            {
                //They can approve so just put it in the tables
                var purchaseOrderEntity = _mapper.Map<EntityFramework.Entities.PurchaseOrder>(command);
                purchaseOrderEntity.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Name == "Open");
                purchaseOrderEntity.Revision = 1;

                await _unitOfWork.PurchaseOrders.AddAsync(purchaseOrderEntity);
                await _unitOfWork.SaveChangesAsync();

                var productEntities = await _unitOfWork.Products.Query()
                    .Where(i => command.Products.Contains(i.Id))
                    .ToListAsync();

                foreach (var productEntity in productEntities)
                {
                    var purchaseOrderProductEntity = new PurchaseOrderProduct()
                    {
                        PurchaseOrder = purchaseOrderEntity,
                        Product = productEntity,
                        ProductRevision = productEntity.Revision
                    };

                    await _unitOfWork.PurchaseOrderProducts.AddAsync(purchaseOrderProductEntity);
                }

                await _unitOfWork.LogApprovalTransaction(purchaseOrderEntity, purchaseOrderEntity.Id, "Approved", "Auto Approved");
                var purchaseOrderView = _mapper.Map<PurchaseOrderView>(purchaseOrderEntity);
                purchaseOrderView.Products = productEntities.Select(i => _mapper.Map<PurchaseOrderProductView>(i)).ToList();
                purchaseOrderView.CustomerName = (await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == command.CustomerId)).Name;

                return purchaseOrderView;
            }
            else
            {
                var purchaseOrderApprovalEntity = _mapper.Map<PurchaseOrderApproval>(command);
                purchaseOrderApprovalEntity.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(purchaseOrderApprovalEntity);
                purchaseOrderApprovalEntity.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(purchaseOrderApprovalEntity.Workflow?.Id ?? 0);
                purchaseOrderApprovalEntity.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                await _unitOfWork.PurchaseOrderApprovals.AddAsync(purchaseOrderApprovalEntity);
                await _unitOfWork.SaveChangesAsync();

                var productEntities = await _unitOfWork.Products.Query()
                    .Where(i => command.Products.Contains(i.Id))
                    .ToListAsync();

                foreach (var productEntity in productEntities)
                {
                    var PurchaseOrderProductApprovalEntity = new PurchaseOrderProductApproval()
                    {
                        PurchaseOrderApprovalId = purchaseOrderApprovalEntity.Id,
                        ProductId = productEntity.Id
                    };

                    await _unitOfWork.PurchaseOrderProductApprovals.AddAsync(PurchaseOrderProductApprovalEntity);
                }

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<PurchaseOrderView>(purchaseOrderApprovalEntity);
            }
        }

        public async Task<PurchaseOrderView> UpdatePurchaseOrderAsync(UpdatePurchaseOrder command)
        {
            var purchaseOrderEntity = await _unitOfWork.PurchaseOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (purchaseOrderEntity is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.PurchaseOrder)} with ID: {command.Id} not found", DomainError.NotFound);
            }

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.PurchaseOrderApproval))
            {
                _mapper.Map(command, purchaseOrderEntity);
                purchaseOrderEntity.Revision = purchaseOrderEntity.Revision == null ? 1 : purchaseOrderEntity.Revision + 1;
                if(command.ClosePurchaseOrder && command.CloseDate.HasValue)
                {
                    purchaseOrderEntity.CloseDate = command.CloseDate;
                    purchaseOrderEntity.StatusId = (int)PurchaseOrderStatusEnum.Closed;
                    purchaseOrderEntity.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)PurchaseOrderStatusEnum.Closed);
                }
                _unitOfWork.PurchaseOrders.Update(purchaseOrderEntity);
                await _unitOfWork.SaveChangesAsync();

                var productIds = await _unitOfWork.PurchaseOrderProducts.Query()
                    .Where(i => i.PurchaseOrderId == purchaseOrderEntity.Id)
                    .Select(i => i.ProductId)
                    .ToListAsync();

                //Exists in DB but not in list: Remove
                var productIdsToRemove = productIds.Except(command.Products);
                //Does not Exist in DB: Add
                var productIdsToAdd = command.Products.Except(productIds);
                var usedProductIds = await _unitOfWork.Purchases.Query()
                    .Where(i => i.PurchaseOrderId == command.Id)
                    .Select(i => i.PurchaseOrderProductId)
                    .ToListAsync();
                var purchaseOrderProductEntitiesToRemove = await _unitOfWork.PurchaseOrderProducts.Query()
                    .Where(i => i.PurchaseOrderId == purchaseOrderEntity.Id && productIdsToRemove.Contains(i.ProductId) && !usedProductIds.Contains(i.Id))
                    .ToListAsync();
                
                foreach (var purchaseOrderProductEntity in purchaseOrderProductEntitiesToRemove)
                {
                    _unitOfWork.PurchaseOrderProducts.Delete(false, purchaseOrderProductEntity);
                }

                await _unitOfWork.SaveChangesAsync();

                foreach (var productIdToAdd in productIdsToAdd)
                {
                    var purchaseOrderProductEntity = new PurchaseOrderProduct()
                    {
                        PurchaseOrderId = purchaseOrderEntity.Id,
                        ProductId = productIdToAdd
                    };

                    await _unitOfWork.PurchaseOrderProducts.AddAsync(purchaseOrderProductEntity);
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(purchaseOrderEntity, purchaseOrderEntity.Id, "Approved", "Auto Approved");
                var purchaseOrderViews = await GetPurchaseOrderAsync(new GetPurchaseOrder() { Id = command.Id });
                return purchaseOrderViews.FirstOrDefault();
            }
            else
            {
                var purchaseOrderApprovalEntity = _mapper.Map<PurchaseOrderApproval>(purchaseOrderEntity);
                _mapper.Map(command, purchaseOrderApprovalEntity);
                purchaseOrderApprovalEntity.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(purchaseOrderApprovalEntity);
                purchaseOrderApprovalEntity.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(purchaseOrderApprovalEntity.Workflow?.Id ?? 0);
                purchaseOrderApprovalEntity.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                await _unitOfWork.PurchaseOrderApprovals.AddAsync(purchaseOrderApprovalEntity);
                await _unitOfWork.SaveChangesAsync();

                var productEntities = _unitOfWork.Products.Query().Where(i => command.Products.Contains(i.Id));

                foreach (var productEntity in productEntities)
                {
                    var purchaseOrderProductApprovalEntity = new PurchaseOrderProductApproval()
                    {
                        PurchaseOrderApprovalId = purchaseOrderApprovalEntity.Id,
                        ProductId = productEntity.Id
                    };

                    await _unitOfWork.PurchaseOrderProductApprovals.AddAsync(purchaseOrderProductApprovalEntity);
                }

                await _unitOfWork.SaveChangesAsync();

                var purchaseOrderViews = await GetPurchaseOrderAsync(new GetPurchaseOrder() { Id = command.Id });
                return purchaseOrderViews.FirstOrDefault();
            }
        }

        public async Task<PurchaseOrderView> DeletePurchaseOrderAsync(DeletePurchaseOrder command)
        {
            var inUse = _unitOfWork.Purchases.Query().Any(i => i.PurchaseOrderId == command.Id);

            if (inUse)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.PurchaseOrder)} in use.", DomainError.Conflict);
            }

            var purchaseOrderEntity = await _unitOfWork.PurchaseOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (purchaseOrderEntity is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.PurchaseOrder)} with ID: {command.Id} not found", DomainError.NotFound);
            }

            var purchaseOrderProductEntitiesToDelete = await _unitOfWork.PurchaseOrderProducts.Query().Where(i => i.PurchaseOrderId == command.Id).ToListAsync();

            foreach (var purchaseOrderProductEntity in purchaseOrderProductEntitiesToDelete)
            {
                _unitOfWork.PurchaseOrderProducts.Delete(false, purchaseOrderProductEntity);
            }

            _unitOfWork.PurchaseOrders.Delete(false, purchaseOrderEntity);
            await _unitOfWork.LogApprovalTransaction(purchaseOrderEntity, purchaseOrderEntity.Id, "Approved", "Auto Approved");

            return _mapper.Map<PurchaseOrderView>(purchaseOrderEntity);
        }

        public async Task<IEnumerable<PurchaseOrderView>> GetPurchaseOrderProductAsync(GetPurchaseOrder command)
        {
            List<PurchaseOrderView> purchaseOrderViews;

            if (command.Id.HasValue)
            {
                purchaseOrderViews = await _unitOfWork.PurchaseOrders
                                            .Query()
                                            .Include(po => po.Customer)
                                            .Select(po => _mapper.Map<PurchaseOrderView>(po))
                                            .Where(po => po.Id == command.Id)
                                            .ToListAsync();
                if (!purchaseOrderViews.Any())
                {
                    throw new DomainException($"PurchaseOrder ID {command.Id} not found", DomainError.NotFound);
                }
            }
            else
            {
                purchaseOrderViews = await _unitOfWork.PurchaseOrders
                                            .Query()
                                            .Include(po => po.Customer)
                                            .Select(po => _mapper.Map<PurchaseOrderView>(po))
                                            .ToListAsync();
            }

            var result = purchaseOrderViews.Select(x => _mapper.Map<PurchaseOrderView>(x)).OrderBy(x => x.Name).AsEnumerable();

            return result;
        }
    }
}
