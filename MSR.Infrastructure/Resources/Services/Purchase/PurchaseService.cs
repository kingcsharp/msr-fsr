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
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<Domain.Models.PurchaseModel>> GetPurchasesAsync(GetPurchases command)
        {
            List<Purchase> purchases = null;
            if (command.Id.HasValue) {
                purchases = await _unitOfWork.Purchases.Query().Where(x => x.Id == command.Id.Value).ToListAsync();
                if (purchases.Count == 0) {
                    throw new DomainException($"procedure ID {command.Id.Value} not found", DomainError.NotFound);
                }
            } else {
                purchases = await _unitOfWork.Purchases.Query().ToListAsync();
            }
            var result = purchases.Select(x => _mapper.Map<Domain.Models.PurchaseModel>(x)).ToList();
            return result;
        }

        public async Task<Domain.Models.Procedure> CreateProcedureAsync(CreateProcedure command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Procedure ret;

            if (user.CanApprove(EnumMenuItem.Procedures))
            {
                Procedure procedure = _mapper.Map<EntityFramework.Entities.Procedure>(command);
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                _unitOfWork.Procedures.Add(procedure);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.Procedure>(procedure);
            }
            else
            {
                var approval = _mapper.Map<ProcedureApproval>(command);
                _unitOfWork.ProcedureApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.Procedure>(approval);
            }

            return ret;
        }
        public async Task<Domain.Models.Procedure> UpdateProcedureAsync(UpdateProcedure command)
        {
            var current = await _unitOfWork.Procedures.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Procedure)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Procedure ret;

            if (user.CanApprove(EnumMenuItem.Procedures))
            {
                var procedure = _mapper.Map(command, current);
                _unitOfWork.Procedures.Update(procedure);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                ret = _mapper.Map<Domain.Models.Procedure>(procedure);
            }
            else
            {
                var approval = _mapper.Map<ProcedureApproval>(command);
                _unitOfWork.ProcedureApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.Procedure>(approval);
            }

            return ret;

        }
        public async Task<ICollection<Domain.Models.ProcedureStepModel>> GetProcedureStepAsync(GetProcedureStep command)
        {
            List<EntityFramework.Entities.ProcedureStep> steps;
            if (command.stepId.HasValue) {
                steps = await _unitOfWork.ProcedureSteps.Query().Where(x => x.Id == command.stepId.Value).ToListAsync();
                if (steps.Count == 0) {
                    throw new DomainException($"step ID {command.stepId.Value} not found", DomainError.NotFound);
                }
            } else {
                steps = await _unitOfWork.ProcedureSteps.Query().Where(x => x.ProcedureId == command.procedureId).ToListAsync();
            }
            var result = steps.Select(x => _mapper.Map<Domain.Models.ProcedureStepModel>(x)).OrderBy(x => x.PrintOrder).ToList();
            return result;
        }

        public async Task<Domain.Models.ProcedureStepModel> CreateProcedureStepAsync(CreateProcedureStep command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepModel ret;

            if (user.CanApprove(EnumMenuItem.Procedures))
            {
                var procstep = _mapper.Map<ProcedureStep>(command);
                _unitOfWork.ProcedureSteps.Add(procstep);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procstep, procstep.Id);

                ret = _mapper.Map<Domain.Models.ProcedureStepModel>(procstep);
            }
            else
            {
                var approval = _mapper.Map<ProcedureStepApproval>(command);
                _unitOfWork.ProcedureStepApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStepModel>(approval);
            }

            return ret;
        }

        public async Task<Domain.Models.ProcedureStepModel> UpdateProcedureStepAsync(UpdateProcedureStep command)
        {
            var current = await _unitOfWork.ProcedureSteps.FirstOrDefaultAsync(false, i => i.Id == command.procedureStepId);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureStep)} not found with ID: {command.procedureStepId}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepModel ret;

            if (user.CanApprove(EnumMenuItem.Procedures))
            {
                var step = _mapper.Map(command, current);
                _unitOfWork.ProcedureSteps.Update(step);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(step, step.Id);

                ret = _mapper.Map<Domain.Models.ProcedureStepModel>(step);
            }
            else
            {
                var approval = _mapper.Map<ProcedureStepApproval>(command);
                _unitOfWork.ProcedureStepApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStepModel>(approval);
            }

            return ret;
        }
    }
}
