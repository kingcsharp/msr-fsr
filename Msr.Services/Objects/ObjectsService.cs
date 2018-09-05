using Msr.Models.Objects;
using Msr.Repositories;
using System.Linq;

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
        public ObjectView GetObjectById(string id)
        {
            return GetObjectsQueryable().Where(x => x.Id == id).SingleOrDefault();
        }

        public IQueryable<ObjectSearchView> GetObjectSearchQueryable()
        {
            return _dbContext.ObjectSearchViews;
        }
    }
}
