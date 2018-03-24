using EntityFrameworkExtras.EF6;
using Msr.Models.Regions;
using Msr.Repositories;
using Msr.Services.Regions.Procedures;
using Msr.Services.Regions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.Comman;

namespace Msr.Services.Regions
{
    public class RegionService
    {
        private readonly MsrDbContext _dbContext;

        public RegionService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<RegionsView> RegionsQueryable
        {
            get
            {
                return _dbContext.RegionsViews;
            }
        }
        public RegionsView GetById(string Id)
        {
            return RegionsQueryable.Where(x => x.ObjectId == Id).SingleOrDefault();
        }
        public bool Save(SaveRegionViewModel model)
        {
            try
            {
                var saveRegionProcedure = new SaveRegionProcedure() { ObjID = model.ObjectId, Name = model.Name, NTLogin = model.LogId };

                _dbContext.Database.ExecuteStoredProcedure(saveRegionProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Create(SaveRegionViewModel model)
        {
            try
            {
                var saveRegionProcedure = new SaveRegionProcedure() { Name = model.Name, NTLogin = model.LogId };

                _dbContext.Database.ExecuteStoredProcedure(saveRegionProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Delete(DeleteRegionViewModel model)
        {
            try
            {
                //TODO
                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public List<SelectFile> GetActiveRegions(string ntLogin)
        {
            var sql = $"EXEC A_SP_REGIONS_SELECT_PAGE ' (NAME LIKE ''%%'' OR NAME is NULL ) AND  (OBJECT_ID LIKE ''%%'' OR OBJECT_ID is NULL )',' ORDER BY NAME','{ntLogin}'";

            var result = _dbContext.Database.SqlQuery<SelectFile>(sql).ToList();

            return result;
        }
    }
}
