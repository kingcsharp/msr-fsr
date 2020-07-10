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

                var e = procedure.ProcedureTypeId;

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
    }
}
