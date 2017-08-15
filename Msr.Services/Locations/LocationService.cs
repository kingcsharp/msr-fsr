using EntityFrameworkExtras.EF6;
using Msr.Models.Locations;
using Msr.Repositories;
using Msr.Services.Locations.Procedures;
using Msr.Services.Locations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Locations
{
    public class LocationService
    {
        private readonly MsrDbContext _dbContext;

        public LocationService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<LocationView> GetLocationsQueryable()
        {
            return _dbContext.LocationViews;
        }

        public LocationView GetById(string Id)
        {
            return GetLocationsQueryable().Where(x => x.ObjectId == Id).SingleOrDefault();
        }

        public bool Save(SaveLocationViewModel model)
        {
            try
            {
                var saveLocationProcedure = new SaveLocationProcedure()
                {
                    ObjID = model.ObjectId,
                    Name = model.Name,
                    Parent = model.Parent,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    State = model.State,
                    Country = model.Country,
                    PostalCode = model.PostalCode,
                    Region = model.Region,
                    InternalAddress = model.InternalAddress,
                    NTLogin = model.LoggedUserIdResult.Id
                };

                _dbContext.Database.ExecuteStoredProcedure(saveLocationProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Create(SaveLocationViewModel model)
        {
            try
            {
                var saveLocationProcedure = new SaveLocationProcedure()
                {
                    Name = model.Name,
                    Parent = model.Parent,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    State = model.State,
                    Country = model.Country,
                    PostalCode = model.PostalCode,
                    Region = model.Region,
                    InternalAddress = model.InternalAddress,
                    NTLogin = model.LoggedUserIdResult.Id
                };

               _dbContext.Database.ExecuteStoredProcedure(saveLocationProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var NTLogin = "1618";
                var deletePartProcedure = new DeleteLocationProcedure() { ObjID = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePartProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public List<LocationResult> GetActiveLocations()
        {
            var sql = @"EXEC A_SP_LOCATIONS_SELECT ' (NAME LIKE ''%%'' OR NAME is NULL ) AND  (OBJECT_ID LIKE ''%%'' OR OBJECT_ID is NULL ) AND  (FULL_ADDRESS LIKE ''%%'' OR FULL_ADDRESS is NULL ) AND  (REGION_NAME LIKE ''%%'' OR REGION_NAME is NULL )',' ORDER BY NAME','1618'";

            var result = _dbContext.Database.SqlQuery<LocationResult>(sql).ToList();

            return result;
        }
    }
}
