using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Infrastructure.Resources.EntityFramework.Application;

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

    }
}
