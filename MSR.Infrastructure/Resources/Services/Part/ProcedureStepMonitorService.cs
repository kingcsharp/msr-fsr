using AutoMapper;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Role
{
    public class ProcedureStepMonitorService : IProcedureStepMonitorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProcedureStepMonitorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<MonitorModel>> GetMonitorModelAsync(GetMonitorModel command)
        {
            var procsteps = await GetProcedureStepMonitorAsync(new GetProcedureStepMonitor(){
                procedureStepMonitorId = command.procedureStepMonitorId
            });

            if (procsteps == null || procsteps.Count == 0) {
                // shouldn't happen because GetProcedureStepMonitorAsync throws first
                throw new DomainException($"procedure ID {command.procedureStepMonitorId} not found", DomainError.NotFound);
            }

            var ret = procsteps.Select(x => _mapper.Map<MonitorModel>(x)).ToList();;

            // TODO: get serial and recorded result?

            return ret;
        }

        public async Task<ICollection<Domain.Models.ProcedureStepMonitor>> GetProcedureStepMonitorAsync(GetProcedureStepMonitor command)
        {
            List<EntityFramework.Entities.ProcedureStepMonitor> procedures;
            if (command.procedureStepMonitorId.HasValue) {
                procedures = await _unitOfWork.ProcedureStepMonitors
                    .Query()
                    .Where(x => x.Id == command.procedureStepMonitorId.Value)
                    .Include(x => x.MonitorType)
                    .ToListAsync();
                if (procedures.Count == 0) {
                    throw new DomainException($"procedure ID {command.procedureStepMonitorId.Value} not found", DomainError.NotFound);
                }
            } else if (command.procedureStepId.HasValue) {
                procedures = await _unitOfWork.ProcedureStepMonitors
                    .Query()
                    .Where(x => x.ProcedureStepId == command.procedureStepId.Value)
                    .Include(x => x.MonitorType)
                    .ToListAsync();
                // note that the return here can be an empty list
            } else {
                procedures = await _unitOfWork.ProcedureStepMonitors
                    .Query()
                    .ToListAsync();
            }
            var result = procedures.Select(x => _mapper.Map<Domain.Models.ProcedureStepMonitor>(x)).OrderBy(x => x.Description).ToList();
            return result;
        }
        public async Task<MSR.Domain.Models.ProcedureStepMonitor> CreateProcedureStepMonitorAsync(CreateProcedureStepMonitor command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepMonitor ret;

            MonitorType mtype = GetMonitorType(command.MonitorType);
            MonitorInputType itype = GetInputType(mtype.Id, command.InputType);

            if (user.CanApprove(EnumMenuItem.Monitors))
            {
                EntityFramework.Entities.ProcedureStepMonitor procedure = _mapper.Map<EntityFramework.Entities.ProcedureStepMonitor>(command);
                procedure.MonitorTypeId = mtype.Id;
                procedure.InputTypeId = itype.Id;

                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                _unitOfWork.ProcedureStepMonitors.Add(procedure);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStepMonitor>(procedure);
            }
            else
            {
                var approval = _mapper.Map<ProcedureStepMonitorApproval>(command);
                _unitOfWork.ProcedureStepMonitorApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStepMonitor>(approval);
            }

            return ret;
        }

        public async Task<Domain.Models.ProcedureStepMonitor> UpdateProcedureStepMonitorAsync(UpdateProcedureStepMonitor command)
        {
            var current = await _unitOfWork.ProcedureStepMonitors.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureStepMonitor)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            MonitorType mtype = null;
            if (!command.MonitorType.IsNullOrEmpty()) {
                mtype = GetMonitorType(command.MonitorType);
                command.MonitorTypeId = mtype.Id;
            }
            MonitorInputType itype = null;
            if (!command.InputType.IsNullOrEmpty()) {
                int mtypeId = current.MonitorTypeId;
                if (mtype != null) {
                    mtypeId = mtype.Id;
                }
                itype = GetInputType(mtypeId, command.InputType);
                command.InputTypeId = itype.Id;
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepMonitor ret;

            if (user.CanApprove(EnumMenuItem.Monitors))
            {
                var procedure = _mapper.Map(command, current);
                _unitOfWork.ProcedureStepMonitors.Update(procedure);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                ret = _mapper.Map<Domain.Models.ProcedureStepMonitor>(procedure);
            }
            else
            {
                var approval = _mapper.Map<ProcedureStepMonitorApproval>(command);
                _unitOfWork.ProcedureStepMonitorApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStepMonitor>(approval);
            }

            return ret;

        }

        // Note that the data set is small here.  This function returns two sets of data:
        // 1. a join of MonitorInputType + MonitorType
        // 2. a join of MonitorListItem + MonitorList
        public async Task<ProcedureStepMonitorDefinition> GetProcedureStepMonitorDefinitionAsync(GetProcedureStepMonitorDefinition command)
        {
            List<MonitorInputType> mons = await _unitOfWork.MonitorInputTypes.Query().ToListAsync();
            List<MonitorListItem> items = await _unitOfWork.MonitorListItems.Query().ToListAsync();
            ProcedureStepMonitorDefinition ret = new ProcedureStepMonitorDefinition();

            ret.Types = mons.Select(x => _mapper.Map<ProcedureStepMonitorInputType>(x)).ToList();
            ret.ListItems = items.Select(x => _mapper.Map<ProcedureStepMonitorListItem>(x)).ToList();

            return ret;
        }

        public async Task<bool> DeleteMonitorModelAsync(DeleteProcedureStepMonitor command)
        {
            var current = await _unitOfWork.ProcedureStepMonitors.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureStepMonitor)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            if (CurrentUser.HasPrivilege(EnumMenuItem.Monitors, EnumPrivilege.CanDelete)) {
                _unitOfWork.ProcedureStepMonitors.Delete(false, current);
            } else {
                throw new DomainException($"Permission denied for user {CurrentUser.GetId()}", DomainError.BadRequest);
            }

            return true;
        }

        private MonitorType GetMonitorType(string monitorTypeName)
        {
            MonitorType mtype = _unitOfWork.MonitorTypes
                .Query()
                .FirstOrDefault(x =>
                    x.Name.ToUpper().Equals(monitorTypeName.ToUpper())
                );
            if (mtype == null) {
                throw new DomainException($"Monitor type {monitorTypeName.ToUpper()} not found", DomainError.NotFound);
            }
            return mtype;
        }

        private MonitorInputType GetInputType(int monId, string inputTypeName)
        {
            MonitorInputType itype = _unitOfWork.MonitorInputTypes
                .Query()
                .FirstOrDefault(x =>
                    x.Name.ToUpper().Equals(inputTypeName.ToUpper()) &&
                    x.MonitorTypeId == monId
                );
            if (itype == null) {
                throw new DomainException(
                    $"Input type {inputTypeName.ToUpper()} not found in monitor ID {monId}",
                    DomainError.NotFound);
            }
            return itype;
        }

    }
}
