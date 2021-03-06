using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using MSR.Infrastructure.Resources.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Part
{
    public class ProcedureTypeService : IProcedureTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProcedureTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<Domain.Models.ProcedureType>> GetProcedureTypeAsync(GetProcedureType command)
        {
            var procedureTypeEntities = await _unitOfWork.ProcedureTypes.Query().CreateProcedureTypeQuery(command).ToListAsync();

            if (procedureTypeEntities.Count == 0)
            {
                throw new DomainException($"procedure ID {command.Id.Value} not found", DomainError.NotFound);
            }

            var procedureTypeModels = procedureTypeEntities.Select(x => _mapper.Map<Domain.Models.ProcedureType>(x)).ToList();
            return procedureTypeModels;
        }
        public async Task<Domain.Models.ProcedureType> CreateProcedureTypeAsync(CreateProcedureType command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureType ret;

            if (user.CanApprove(EnumMenuItem.Monitors))
            {
                EntityFramework.Entities.ProcedureType procedure = _mapper.Map<EntityFramework.Entities.ProcedureType>(command);
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                _unitOfWork.ProcedureTypes.Add(procedure);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureType>(procedure);
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureType)} uid {user.Id}");
            }

            return ret;
        }
        public async Task<Domain.Models.ProcedureType> UpdateProcedureTypeAsync(UpdateProcedureType command)
        {
            var current = await _unitOfWork.ProcedureTypes.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureType)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureType ret;

            if (user.CanApprove(EnumMenuItem.Monitors))
            {
                var procedure = _mapper.Map(command, current);
                _unitOfWork.ProcedureTypes.Update(procedure);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                ret = _mapper.Map<Domain.Models.ProcedureType>(procedure);
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureType)} uid {user.Id}");
            }

            return ret;

        }

        public async Task<bool> DeleteProcedureTypeAsync(DeleteProcedureType command)
        {
            var current = await _unitOfWork.ProcedureTypes.FirstOrDefaultAsync(false, i => i.Id == command.id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureType)} not found with ID: {command.id}", DomainError.NotFound);
            }

            if (CurrentUser.HasPrivilege(EnumMenuItem.ProcedureTypes, EnumPrivilege.CanDelete))
            {
                _unitOfWork.ProcedureTypes.Delete(false, current);
                await _unitOfWork.SaveChangesAsync();
            } else {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureType)} uid {CurrentUser.GetId()}");
            }

            return true;
        }

        public async Task<int> GetProcedureTypeTotalRows(GetProcedureType command)
        {
            var totalRows = await _unitOfWork.ProcedureTypes.Query().CreateProcedureTypeQuery(command).CountAsync();

            return totalRows;

        }
    }
}
