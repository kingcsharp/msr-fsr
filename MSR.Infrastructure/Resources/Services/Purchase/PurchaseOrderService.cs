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
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var purchaseOrders = _unitOfWork.PurchaseOrders.Query();
            if (command.Id.HasValue)
            {
                purchaseOrders = purchaseOrders.Where(i => i.Id == command.Id);
            }

            var purchaseOrderList = await purchaseOrders.Select(po => new PurchaseOrderView
            {
                Name = po.Name,
                CustomerId = po.CustomerId,
                CustomerReferencePO = po.CustomerReference,
                OpenDate = po.OpenDate,
                CloseDate = po.CloseDate
            }).ToListAsync();
            
            var purchaseOrderIds = purchaseOrderList.Select(i => i.Id);
            var purchaseOrderCustomerIds = purchaseOrderList.Select(i => i.CustomerId);

            var products = await _unitOfWork.Products.Query().Where(x => _unitOfWork.PurchaseOrderProducts.Query()
                    .Select(i => i.ProductId).Contains(x.Id))
                .Select(i => _mapper.Map<PurchaseOrderProductView>(i))
                .ToListAsync();

            var customers = await _unitOfWork.Customers.Query()
                .Where(i => purchaseOrderCustomerIds.Contains(i.Id))
                .Select(i => new { i.Id, i.Name })
                .ToDictionaryAsync(i => i.Id, i => i.Name);

            var purchaseOrderIdsForDeletable = await _unitOfWork.Purchases.Query().Where(i => purchaseOrderIds.Contains(i.PurchaseOrderId))
                .Select(x => x.PurchaseOrderId).ToListAsync();

            foreach (var po in purchaseOrderList)
            {
                customers.TryGetValue(po.CustomerId, out var name);
                po.Products = products.Where(i => i.Id == po.Id).ToList();
                if (name != null)
                {
                    po.CustomerName = name;
                }
                po.IsDeletable = purchaseOrderIdsForDeletable.All(i => i != po.Id);
            }

            return purchaseOrderList;
        }

        public async Task<PurchaseOrderView> CreatePurchaseOrderAsync(CreatePurchaseOrder command)
        {
            //First, Check if the user has CanApprove or not.
            if (CurrentUser.CanApproveActivity(EnumApprovalTables.PurchaseOrderApproval))
            {
                //They can approve so just put it in the tables
                var purchaseOrder = _mapper.Map<EntityFramework.Entities.PurchaseOrder>(command);

                await _unitOfWork.PurchaseOrders.AddAsync(purchaseOrder);
                await _unitOfWork.SaveChangesAsync();

                var products = _unitOfWork.Products.Query().Where(i => command.Products.Contains(i.Id));

                foreach (var product in products)
                {
                    var map = new PurchaseOrderProduct()
                    {
                        PurchaseOrder = purchaseOrder,
                        Product = product,
                        ProductRevision = product.Revision
                    };

                    await _unitOfWork.PurchaseOrderProducts.AddAsync(map);
                }

                await _unitOfWork.LogApprovalTransaction(purchaseOrder, purchaseOrder.Id, "Approved", "Auto Approved");
                return _mapper.Map<PurchaseOrderView>(purchaseOrder);
            }
            else
            {
                var purchaseOrderApproval = _mapper.Map<PurchaseOrderApproval>(command);
                purchaseOrderApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(purchaseOrderApproval);
                purchaseOrderApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(purchaseOrderApproval.Workflow?.Id ?? 0);
                purchaseOrderApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                await _unitOfWork.PurchaseOrderApprovals.AddAsync(purchaseOrderApproval);
                await _unitOfWork.SaveChangesAsync();

                var products = _unitOfWork.Products.Query().Where(i => command.Products.Contains(i.Id));

                foreach (var product in products)
                {
                    var approvalMap = new PurchaseOrderProductApproval()
                    {
                        PurchaseOrderApprovalId = purchaseOrderApproval.Id,
                        ProductId = product.Id
                    };

                    await _unitOfWork.PurchaseOrderProductApprovals.AddAsync(approvalMap);
                }

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<PurchaseOrderView>(purchaseOrderApproval);
            }
        }

        public async Task<PurchaseOrderView> UpdatePurchaseOrderAsync(UpdatePurchaseOrder command)
        {
            var purchaseOrder = await _unitOfWork.PurchaseOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (purchaseOrder is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.PurchaseOrder)} with ID: {command.Id} not found", DomainError.NotFound);
            }

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.PurchaseOrderApproval))
            {
                _mapper.Map(command, purchaseOrder);

                _unitOfWork.PurchaseOrders.Update(purchaseOrder);

                var productIds = _unitOfWork.PurchaseOrderProducts.Query().Where(i => i.PurchaseOrderId == purchaseOrder.Id).Select(i => i.ProductId);

                //Exists in DB but not in list: Remove
                var productsToRemove = productIds.Except(command.Products);
                //Does not Exist in DB: Add
                var productsToAdd = command.Products.Except(productIds);

                var removeProducts = await _unitOfWork.PurchaseOrderProducts.Query().Where(i => i.PurchaseOrderId == purchaseOrder.Id && productsToRemove.Contains(i.ProductId)).ToListAsync();
                foreach (var removeProduct in removeProducts)
                {
                    _unitOfWork.PurchaseOrderProducts.Delete(false, removeProduct);
                }

                foreach (var addProduct in productsToAdd)
                {
                    var map = new PurchaseOrderProduct()
                    {
                        PurchaseOrderId = purchaseOrder.Id,
                        ProductId = addProduct
                    };

                    await _unitOfWork.PurchaseOrderProducts.AddAsync(map);
                }

                await _unitOfWork.LogApprovalTransaction(purchaseOrder, purchaseOrder.Id, "Approved", "Auto Approved");
                return _mapper.Map<PurchaseOrderView>(purchaseOrder);
            }
            else
            {
                var poApproval = _mapper.Map<PurchaseOrderApproval>(purchaseOrder);
                _mapper.Map(command, poApproval);

                await _unitOfWork.PurchaseOrderApprovals.AddAsync(poApproval);
                await _unitOfWork.SaveChangesAsync();

                var products = _unitOfWork.Products.Query().Where(i => command.Products.Contains(i.Id));

                foreach (var product in products)
                {
                    var approvalMap = new PurchaseOrderProductApproval()
                    {
                        PurchaseOrderApprovalId = poApproval.Id,
                        ProductId = product.Id
                    };

                    await _unitOfWork.PurchaseOrderProductApprovals.AddAsync(approvalMap);
                }

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<PurchaseOrderView>(poApproval);
            }
        }

        public async Task DeletePurchaseOrderAsync(DeletePurchaseOrder command)
        {
            var inUse = _unitOfWork.Purchases.Query().Any(i => i.PurchaseOrderId == command.Id);

            if (inUse)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.PurchaseOrder)} in use.", DomainError.Conflict);
            }

            var purchaseOrder = await _unitOfWork.PurchaseOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (purchaseOrder is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.PurchaseOrder)} with ID: {command.Id} not found", DomainError.NotFound);
            }

            var poProductsToDelete = await _unitOfWork.PurchaseOrderProducts.Query().Where(i => i.PurchaseOrderId == command.Id).ToListAsync();

            foreach (var poProduct in poProductsToDelete)
            {
                //_unitOfWork.PurchaseOrderProducts.Delete(poProduct);
            }

            _unitOfWork.PurchaseOrders.Delete(false, purchaseOrder);
            await _unitOfWork.LogApprovalTransaction(purchaseOrder, purchaseOrder.Id, "Approved", "Auto Approved");
        }

        public async Task<IEnumerable<PurchaseOrderView>> GetPurchaseOrderProductAsync(GetPurchaseOrder command)
        {
            List<PurchaseOrderView> poList;

            if (command.Id.HasValue)
            {
                poList = await _unitOfWork.PurchaseOrders
                                            .Query()
                                            .Include(po => po.Customer)
                                            .Select(po => _mapper.Map<PurchaseOrderView>(po))
                                            .Where(po => po.Id == command.Id)
                                            .ToListAsync();
                if (!poList.Any())
                {
                    throw new DomainException($"PurchaseOrder ID {command.Id} not found", DomainError.NotFound);
                }
            }
            else
            {
                poList = await _unitOfWork.PurchaseOrders
                                            .Query()
                                            .Include(po => po.Customer)
                                            .Select(po => _mapper.Map<PurchaseOrderView>(po))
                                            .ToListAsync();
            }

            var result = poList.Select(x => _mapper.Map<PurchaseOrderView>(x)).OrderBy(x => x.Name).AsEnumerable();

            return result;
        }

        //public async Task<Domain.Models.Procedure> CreateProcedureAsync(CreateProcedure command)
        //{
        //    var user = await _unitOfWork.GetLoggedInUserAsync();
        //    Domain.Models.Procedure ret;

        //    if (user.CanApprove(EnumMenuItem.Procedures))
        //    {
        //        Procedure procedure = _mapper.Map<EntityFramework.Entities.Procedure>(command);
        //        await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

        //        _unitOfWork.Procedures.Add(procedure);
        //        await _unitOfWork.SaveChangesAsync();

        //        ret = _mapper.Map<Domain.Models.Procedure>(procedure);
        //    }
        //    else
        //    {
        //        var approval = _mapper.Map<ProcedureApproval>(command);
        //        _unitOfWork.ProcedureApprovals.Add(approval);
        //        await _unitOfWork.SaveChangesAsync();

        //        ret = _mapper.Map<Domain.Models.Procedure>(approval);
        //    }

        //    return ret;
        //}
        //public async Task<Domain.Models.Procedure> UpdateProcedureAsync(UpdateProcedure command)
        //{
        //    var current = await _unitOfWork.Procedures.FirstOrDefaultAsync(false, i => i.Id == command.Id);

        //    if(current is null)
        //    {
        //        throw new DomainException($"{nameof(EntityFramework.Entities.Procedure)} not found with ID: {command.Id}", DomainError.NotFound);
        //    }

        //    var user = await _unitOfWork.GetLoggedInUserAsync();
        //    Domain.Models.Procedure ret;

        //    if (user.CanApprove(EnumMenuItem.Procedures))
        //    {
        //        var procedure = _mapper.Map(command, current);
        //        _unitOfWork.Procedures.Update(procedure);

        //        // This will call SaveChangesAsync
        //        await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

        //        ret = _mapper.Map<Domain.Models.Procedure>(procedure);
        //    }
        //    else
        //    {
        //        var approval = _mapper.Map<ProcedureApproval>(command);
        //        _unitOfWork.ProcedureApprovals.Add(approval);
        //        await _unitOfWork.SaveChangesAsync();

        //        ret = _mapper.Map<Domain.Models.Procedure>(approval);
        //    }

        //    return ret;

        //}
        //public async Task<ICollection<Domain.Models.ProcedureStep>> GetProcedureStepAsync(GetProcedureStep command)
        //{
        //    List<EntityFramework.Entities.ProcedureStep> steps;
        //    if (command.stepId.HasValue) {
        //        steps = await _unitOfWork.ProcedureSteps.Query().Where(x => x.Id == command.stepId.Value).ToListAsync();
        //        if (steps.Count == 0) {
        //            throw new DomainException($"step ID {command.stepId.Value} not found", DomainError.NotFound);
        //        }
        //    } else {
        //        steps = await _unitOfWork.ProcedureSteps.Query().Where(x => x.ProcedureId == command.procedureId).ToListAsync();
        //    }
        //    var result = steps.Select(x => _mapper.Map<Domain.Models.ProcedureStep>(x)).OrderBy(x => x.PrintOrder).ToList();
        //    return result;
        //}

        //public async Task<Domain.Models.ProcedureStep> CreateProcedureStepAsync(CreateProcedureStep command)
        //{
        //    var user = await _unitOfWork.GetLoggedInUserAsync();
        //    Domain.Models.ProcedureStep ret;

        //    if (user.CanApprove(EnumMenuItem.Procedures))
        //    {
        //        var procstep = _mapper.Map<ProcedureStep>(command);
        //        _unitOfWork.ProcedureSteps.Add(procstep);

        //        // This will call SaveChangesAsync
        //        await _unitOfWork.LogApprovalTransaction(procstep, procstep.Id);

        //        ret = _mapper.Map<Domain.Models.ProcedureStep>(procstep);
        //    }
        //    else
        //    {
        //        var approval = _mapper.Map<ProcedureStepApproval>(command);
        //        _unitOfWork.ProcedureStepApprovals.Add(approval);
        //        await _unitOfWork.SaveChangesAsync();

        //        ret = _mapper.Map<Domain.Models.ProcedureStep>(approval);
        //    }

        //    return ret;
        //}

        //public async Task<Domain.Models.ProcedureStep> UpdateProcedureStepAsync(UpdateProcedureStep command)
        //{
        //    var current = await _unitOfWork.ProcedureSteps.FirstOrDefaultAsync(false, i => i.Id == command.procedureStepId);

        //    if(current is null)
        //    {
        //        throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureStep)} not found with ID: {command.procedureStepId}", DomainError.NotFound);
        //    }

        //    var user = await _unitOfWork.GetLoggedInUserAsync();
        //    Domain.Models.ProcedureStep ret;

        //    if (user.CanApprove(EnumMenuItem.Procedures))
        //    {
        //        var step = _mapper.Map(command, current);
        //        _unitOfWork.ProcedureSteps.Update(step);

        //        // This will call SaveChangesAsync
        //        await _unitOfWork.LogApprovalTransaction(step, step.Id);

        //        ret = _mapper.Map<Domain.Models.ProcedureStep>(step);
        //    }
        //    else
        //    {
        //        var approval = _mapper.Map<ProcedureStepApproval>(command);
        //        _unitOfWork.ProcedureStepApprovals.Add(approval);
        //        await _unitOfWork.SaveChangesAsync();

        //        ret = _mapper.Map<Domain.Models.ProcedureStep>(approval);
        //    }

        //    return ret;
        //}
    }
}
