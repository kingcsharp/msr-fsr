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

        public LocationView GetById(string id)
        {
            return _dbContext.LocationViews.SingleOrDefault(x => x.ObjectId == id);
        }
        public bool Save(SaveLocationVM model)
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
                    NTLogin = model.NTLogin
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
        public bool Create(SaveLocationVM model)
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
                    NTLogin = model.NTLogin
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
    }
}
