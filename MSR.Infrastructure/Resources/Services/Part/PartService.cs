using AutoMapper;
using AutoMapper.Internal;
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
    public class PartService : IPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<PartModel>> GetPartsAsync(GetParts command)
        {
            List<Part> parts;
            if (command.partID.HasValue)
            {
                parts = await _unitOfWork.Parts.Query().Where(x => x.Id == command.partID.Value).ToListAsync();
                if (parts.Count == 0)
                {
                    throw new DomainException($"part ID {command.partID.Value} not found", DomainError.NotFound);
                }
            }
            else
            {
                parts = await _unitOfWork.Parts.Query().Include(x => x.Subparts).ToListAsync();
            }
            var result = parts.Select(x => _mapper.Map<PartModel>(x)).OrderBy(x => x.Name).ToList();
            return result;
        }
        public async Task<PartModel> CreatePartAsync(CreatePart command)
        {
            PartModel ret;

            if (DelegateHandler.HasPrivilege(EnumMenuItem.Parts, EnumPrivilege.CanApprove))
            {
                List<PartSubPartMap> children = command.SubParts.Select(x =>
                    _mapper.Map<PartSubPartMap>(x)
                ).ToList();

                Part part = _mapper.Map<Part>(command);
                if (children.Count > 0) {
                    part.IsKit = true;
                }
                _unitOfWork.Parts.Add(part);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(part, part.Id);

                if (children.Count > 0) {
                    foreach (var child in children) {
                        child.ParentPartId = part.Id;
                        child.CreatedBy = part.CreatedBy;
                        child.CreatedOn = part.CreatedOn;
                        _unitOfWork.PartSubPartMaps.Add(child);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                ret = _mapper.Map<PartModel>(part);
            }
            else
            {
                var approval = _mapper.Map<PartApproval>(command);
                _unitOfWork.PartApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<PartModel>(approval);
            }

            return ret;
        }
        public async Task<PartModel> UpdatePartAsync(UpdatePart command)
        {
            var current = await _unitOfWork.Parts.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (current is null)
            {
                throw new DomainException($"{nameof(Part)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            PartModel ret;

            if (DelegateHandler.HasPrivilege(EnumMenuItem.Parts, EnumPrivilege.CanApprove))
            {
                var part = _mapper.Map(command, current);
                await _unitOfWork.LogApprovalTransaction(part, part.Id);

                _unitOfWork.Parts.Update(part);
                await _unitOfWork.SaveChangesAsync();
                ret = _mapper.Map<PartModel>(part);
            }
            else
            {
                var approval = _mapper.Map<PartApproval>(command);
                _unitOfWork.PartApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();
                ret = _mapper.Map<PartModel>(approval);
            }

            return ret;

        }
    }
}
