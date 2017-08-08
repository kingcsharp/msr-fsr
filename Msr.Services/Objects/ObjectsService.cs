using Msr.Models.Objects;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Objects
{
    public class ObjectsService
    {
        private readonly MsrDbContext _dbContext;

        public ObjectsService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ObjectView> GetObjectsQueryable()
        {
            return _dbContext.ObjectViews.OrderBy(x => x.ObjectTable);
        }
    }
}
