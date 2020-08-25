using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Role
{
    public class ProcedureService : IProcedureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProcedureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<Domain.Models.Procedure>> GetProcedureAsync(GetProcedure command)
        {
            List<EntityFramework.Entities.Procedure> procedures;
            if (command.procedureID.HasValue) {
                procedures = await _unitOfWork.Procedures.Query().Where(x => x.Id == command.procedureID.Value).ToListAsync();
                if (procedures.Count == 0) {
                    throw new DomainException($"procedure ID {command.procedureID.Value} not found", DomainError.NotFound);
                }
            } else {
                procedures = await _unitOfWork.Procedures.Query().ToListAsync();
            }
            var result = procedures.Select(x => _mapper.Map<Domain.Models.Procedure>(x)).OrderBy(x => x.Name).ToList();
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

                var created = _unitOfWork.Procedures.Add(procedure);
                await _unitOfWork.SaveChangesAsync();

                // The API return expects the procedure type object to be loaded.
                created.Context.Entry(procedure).Reference(x => x.ProcedureType).Load();

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
            var current = await _unitOfWork.Procedures
                .Query()
                .Where(i => i.Id == command.Id)
                .Include(x => x.ProcedureType)
                .FirstOrDefaultAsync();

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
        public async Task<ICollection<Domain.Models.ProcedureStep>> GetProcedureStepAsync(GetProcedureStep command)
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
            var result = steps.Select(x => _mapper.Map<Domain.Models.ProcedureStep>(x)).OrderBy(x => x.PrintOrder).ToList();
            return result;
        }

        public async Task<Domain.Models.ProcedureStep> CreateProcedureStepAsync(CreateProcedureStep command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStep ret;

            if (user.CanApprove(EnumMenuItem.Procedures))
            {
                var procstep = _mapper.Map<ProcedureStep>(command);
                _unitOfWork.ProcedureSteps.Add(procstep);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procstep, procstep.Id);

                ret = _mapper.Map<Domain.Models.ProcedureStep>(procstep);
            }
            else
            {
                var approval = _mapper.Map<ProcedureStepApproval>(command);
                _unitOfWork.ProcedureStepApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStep>(approval);
            }

            return ret;
        }

        public async Task<Domain.Models.ProcedureStep> UpdateProcedureStepAsync(UpdateProcedureStep command)
        {
            var current = await _unitOfWork.ProcedureSteps.FirstOrDefaultAsync(false, i => i.Id == command.procedureStepId);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureStep)} not found with ID: {command.procedureStepId}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStep ret;

            if (user.CanApprove(EnumMenuItem.Procedures))
            {
                var step = _mapper.Map(command, current);
                _unitOfWork.ProcedureSteps.Update(step);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(step, step.Id);

                ret = _mapper.Map<Domain.Models.ProcedureStep>(step);
            }
            else
            {
                var approval = _mapper.Map<ProcedureStepApproval>(command);
                _unitOfWork.ProcedureStepApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStep>(approval);
            }

            return ret;
        }

        public async Task<bool> DeleteProcedureAsync(DeleteProcedure command)
        {
            var current = await _unitOfWork.Procedures.FirstOrDefaultAsync(false, i => i.Id == command.procedureID);
            if(current is null)
            {
                throw new DomainException($"{nameof(Procedure)} not found with ID: {command.procedureID}", DomainError.NotFound);
            }
            if (CurrentUser.HasPrivilege(EnumMenuItem.Procedures, EnumPrivilege.CanDelete)) {
                _unitOfWork.Procedures.Delete(false, current);
                await _unitOfWork.SaveChangesAsync();
            } else {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.Procedure)} uid {CurrentUser.GetId()}");
            }

            return true;
        }

        public async Task<bool> DeleteProcedureStepAsync(DeleteProcedureStep command)
        {
            var current = await _unitOfWork.ProcedureSteps.FirstOrDefaultAsync(false,
                i => i.ProcedureId == command.procedureID && i.Id == command.procedureStepID
            );
            if(current is null)
            {
                throw new DomainException($"{nameof(ProcedureStep)} not found with ID: {command.procedureID}/{command.procedureStepID}", DomainError.NotFound);
            }
            if (CurrentUser.HasPrivilege(EnumMenuItem.Procedures, EnumPrivilege.CanDelete)) {
                _unitOfWork.ProcedureSteps.Delete(false, current);
                await _unitOfWork.SaveChangesAsync();
            } else {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureStep)} uid {CurrentUser.GetId()}");
            }

            return true;
        }

        public async Task<ICollection<Domain.Models.ProcedureStepTypeModel>> GetProcedureStepType(GetProcedureStepType command)
        {
            List<ProcedureStepType> current;

            if (command.Id.HasValue) {
                current = await _unitOfWork.ProcedureStepTypes.Query().Where(
                    i => i.Id == command.Id
                ).ToListAsync();
            } else {
                current = await _unitOfWork.ProcedureStepTypes.Query().ToListAsync();
            }
            if(current is null || current.Count == 0)
            {
                throw new DomainException($"{nameof(ProcedureStepType)} not found with ID: {command.Id}", DomainError.NotFound);
            }
            return current.Select(x => _mapper.Map<Domain.Models.ProcedureStepTypeModel>(x)).ToList();
        }
    }
}
