using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Role
{
    public class ProcedureStepTemplateService : IProcedureStepTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProcedureStepTemplateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<Domain.Models.ProcedureStepTemplate>> GetProcedureStepTemplateAsync(GetProcedureStepTemplate command)
        {
            List<EntityFramework.Entities.ProcedureStepTemplate> procedures;
            if (command.Id.HasValue) {
                procedures = await _unitOfWork.ProcedureStepTemplates.Query().Where(x => x.Id == command.Id.Value).ToListAsync();
                if (procedures.Count == 0) {
                    throw new DomainException($"procedure ID {command.Id.Value} not found", DomainError.NotFound);
                }
            } else {
                procedures = await _unitOfWork.ProcedureStepTemplates.Query().ToListAsync();
            }
            var result = procedures.Select(x => _mapper.Map<Domain.Models.ProcedureStepTemplate>(x)).OrderBy(x => x.Id).ToList();
            return result;
        }
        public async Task<Domain.Models.ProcedureStepTemplate> CreateProcedureStepTemplateAsync(CreateProcedureStepTemplate command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepTemplate ret;

            if (user.CanApprove(EnumMenuItem.Monitors))
            {
                EntityFramework.Entities.ProcedureStepTemplate procedure = _mapper.Map<EntityFramework.Entities.ProcedureStepTemplate>(command);
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                _unitOfWork.ProcedureStepTemplates.Add(procedure);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStepTemplate>(procedure);
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureStepTemplate)} uid {user.Id}");
            }

            return ret;
        }
        public async Task<Domain.Models.ProcedureStepTemplate> UpdateProcedureStepTemplateAsync(UpdateProcedureStepTemplate command)
        {
            var current = await _unitOfWork.ProcedureStepTemplates.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureStepTemplate)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepTemplate ret;

            if (user.CanApprove(EnumMenuItem.Monitors))
            {
                var procedure = _mapper.Map(command, current);
                _unitOfWork.ProcedureStepTemplates.Update(procedure);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                ret = _mapper.Map<Domain.Models.ProcedureStepTemplate>(procedure);
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureStepTemplate)} uid {user.Id}");
            }

            return ret;

        }
    }
}
