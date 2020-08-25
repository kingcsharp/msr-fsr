using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MSR.Infrastructure.Resources.Services.Timezone
{
    public class TimezoneService : ITimezoneService
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public TimezoneService(IUnitOfWork unitOfwork, IMapper mapper)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
        }
        public async Task<ICollection<TimeZoneModel>> GetTimezone(GetTimezone command)
        {
            var timezones = await _unitOfwork.Timezones.Query()
                .Select(i => _mapper.Map<TimeZoneModel>(i)).ToListAsync();
            return timezones;
        }
    }
}
