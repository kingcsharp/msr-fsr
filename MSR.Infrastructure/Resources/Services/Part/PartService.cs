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
    public class PartService : IPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<Domain.Models.Part>> GetPartsAsync(GetParts command)
        {
            List<EntityFramework.Entities.Part> parts;
            if (command.partID.HasValue) {
                parts = await _unitOfWork.Parts.Query().Where(x => x.Id == command.partID.Value).ToListAsync();
                if (parts.Count == 0) {
                    throw new DomainException($"part ID {command.partID.Value} not found", DomainError.NotFound);
                }
            } else {
                parts = await _unitOfWork.Parts.Query().ToListAsync();
            }
            var result = parts.Select(x => _mapper.Map<Domain.Models.Part>(x)).OrderBy(x => x.Name).ToList();
            return result;
        }
        public async Task<Domain.Models.Part> CreatePartAsync(CreatePart command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Part ret;

            if (user.CanApprove(EnumMenuItem.Parts))
            {
                Part part = _mapper.Map<EntityFramework.Entities.Part>(command);
                await _unitOfWork.LogApprovalTransaction(part, part.Id);

                _unitOfWork.Parts.Add(part);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.Part>(part);
            }
            else
            {
                var approval = _mapper.Map<PartApproval>(command);
                _unitOfWork.PartApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.Part>(approval);
            }

            return ret;
        }
        public async Task<Domain.Models.Part> UpdatePartAsync(UpdatePart command)
        {
            var current = await _unitOfWork.Parts.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Part)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Part ret;

            if (user.CanApprove(EnumMenuItem.Parts))
            {
                var part = _mapper.Map(command, current);
                await _unitOfWork.LogApprovalTransaction(part, part.Id);

                _unitOfWork.Parts.Update(part);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.Part>(part);
            }
            else
            {
                var approval = _mapper.Map<PartApproval>(command);
                _unitOfWork.PartApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.Part>(approval);
            }

            return ret;

        }
    }
}
