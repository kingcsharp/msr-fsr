using System.Linq;
using Msr.Models.Users;
using Msr.Repositories;

namespace Msr.Services.Orders
{
   public class CompanyService
    {
        private readonly MsrDbContext _dbContext;

        public CompanyService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<CompanyView> GetCompanyQueryable()
        {
            return _dbContext.CompanyView;
        }
    }
}
