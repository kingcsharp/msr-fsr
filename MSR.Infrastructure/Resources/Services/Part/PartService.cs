using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
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
            throw new DomainException("unimplemented");
        }
        public async Task<Domain.Models.Part> UpdatePartAsync(UpdatePart command)
        {
            throw new DomainException("unimplemented");
        }
    }
}
