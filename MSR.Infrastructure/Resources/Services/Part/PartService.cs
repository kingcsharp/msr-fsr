using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
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
            List<EntityFramework.Entities.Part> parts = await _unitOfWork.Parts.Query().ToListAsync();
            var result = parts.Select(x => _mapper.Map<Domain.Models.Part>(x)).OrderBy(x => x.Name).ToList();
            return result;
        }
    }
}
