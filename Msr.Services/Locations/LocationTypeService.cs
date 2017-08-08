using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Locations
{
  public class LocationTypeService
    {
        private readonly MsrDbContext _dbContext;

        public LocationTypeService()
        {
            _dbContext = new MsrDbContext();
        }
    }
}
