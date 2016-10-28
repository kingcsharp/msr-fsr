using System.Linq;
using Msr.Models.TimeZones;
using Msr.Repositories;

namespace Msr.Services.TimeZones
{
    public class TimeZoneService
    {
        private readonly MsrDbContext _dbContext;

        public TimeZoneService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<TimeZoneView> GetTimeZoneQueryable()
        {
            return _dbContext.TimeZoneView.OrderBy(x => x.Description);
        }
    }
}
