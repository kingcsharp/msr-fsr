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

        public async Task<ICollection<Domain.Models.ProcedureStepMonitor>> GetProcedureStepMonitorAsync(GetProcedureStepMonitor command)
        {
            List<EntityFramework.Entities.ProcedureStepMonitor> procedures;
            if (command.procedureStepMonitorId.HasValue) {
                procedures = await _unitOfWork.ProcedureStepMonitors.Query().Where(x => x.Id == command.procedureStepMonitorId.Value).ToListAsync();
                if (procedures.Count == 0) {
                    throw new DomainException($"procedure ID {command.procedureStepMonitorId.Value} not found", DomainError.NotFound);
                }
            } else {
                procedures = await _unitOfWork.ProcedureStepMonitors.Query().ToListAsync();
            }
            var result = procedures.Select(x => _mapper.Map<Domain.Models.ProcedureStepMonitor>(x)).OrderBy(x => x.Description).ToList();
            return result;
        }
        public async Task<Domain.Models.ProcedureStepMonitor> CreateProcedureStepMonitorAsync(CreateProcedureStepMonitor command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepMonitor ret;

            if (user.CanApprove(EnumMenuItem.Monitors))
            {
                ProcedureStepMonitor procedure = _mapper.Map<EntityFramework.Entities.ProcedureStepMonitor>(command);
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
    }
}
