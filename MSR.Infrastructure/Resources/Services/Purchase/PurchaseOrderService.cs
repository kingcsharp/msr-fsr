using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
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

        public async Task<IEnumerable<Domain.Models.PurchaseOrderView>> GetPurchaseOrderAsync(GetPurchaseOrder command)
        {
            List<Domain.Models.PurchaseOrderView> poList;

            if (command.Id.HasValue)
            {
                //poList = await _unitOfWork.PurchaseOrderProducts
                //                            .Query()
                //                            .Include(pop => pop.Product)
                //                            .Include(pop => pop.PurchaseOrder)
                //                                .ThenInclude(p => p.Customer)
                //                            .Where(pop => pop.PurchaseOrderId == command.Id)
                //                            .Select(pop => _mapper.Map<Domain.Models.PurchaseOrderView>(pop))
                //                            .ToListAsync();

                poList = await _unitOfWork.PurchaseOrderProducts
                                        .Query()
                                        .Include(pop => pop.Product)
                                        .Include(pop => pop.PurchaseOrder)
                                            .ThenInclude(p => p.Customer)
                                        .Where(pop => pop.PurchaseOrderId == command.Id)
                                        .Select(pop => _mapper.Map<Domain.Models.PurchaseOrderView>(pop))
                                        .ToListAsync();

                if (!poList.Any())
                {
                    throw new DomainException($"PurchaseOrder ID {command.Id} not found", DomainError.NotFound);
                }
            }
            else
            {
                poList = await _unitOfWork.PurchaseOrderProducts
                                            .Query()
                                            .Include(pop => pop.Product)
                                            .Include(pop => pop.PurchaseOrder)
                                                .ThenInclude(p => p.Customer)
                                            .Select(pop => _mapper.Map<Domain.Models.PurchaseOrderView>(pop))
                                            .ToListAsync();
            }

            //poList.ForEach(po =>
            //{
            //    po.Products = new List<>
            //})

            return poList;
        }

        public async Task<IEnumerable<Domain.Models.PurchaseOrderView>> GetPurchaseOrderProductAsync(GetPurchaseOrder command)
        {
            List<Domain.Models.PurchaseOrderView> poList;

            if (command.Id.HasValue)
            {
                poList = await _unitOfWork.PurchaseOrders
                                            .Query()
                                            .Include(po => po.Customer)
                                            .Select(po => _mapper.Map<Domain.Models.PurchaseOrderView>(po))
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
                                            .Select(po => _mapper.Map<Domain.Models.PurchaseOrderView>(po))
                                            .ToListAsync();
            }

            var result = poList.Select(x => _mapper.Map<Domain.Models.PurchaseOrderView>(x)).OrderBy(x => x.Name).AsEnumerable();

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
