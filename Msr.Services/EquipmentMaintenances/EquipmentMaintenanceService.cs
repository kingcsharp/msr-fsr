using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Models.EquipmentMaintenances;
using Msr.Models.Locations;
using Msr.Repositories;
using Msr.Services.EquipmentMaintenances.Procedures;
using Msr.Services.EquipmentMaintenances.ViewModels;

namespace Msr.Services.EquipmentMaintenances
{
    public class EquipmentMaintenanceService
    {
        private readonly MsrDbContext _dbContext;

        public EquipmentMaintenanceService()
        {
            _dbContext = new MsrDbContext();
        }

        public ResultNotification<string> Create(CreateEquipmentMaintenanceViewModel model)
        {
            var response = new ResultNotification<string>();

            try
            {
                var savePartProcedure = new CreateEquipmantMaintenanceProcedure()
                {
                    Id = model.Id,
                    ScanBarcode = model.ScanBarcode,
                    PrimaryLocationId = model.PrimaryLocationId,
                    SubLocationFirstId = model.SubLocationFirstId,
                    SubLocationSecondId = model.SubLocationSecondId,
                    DateTime = model.DateTime,
                    TroubleState = model.TroubleState,
                    Technician = model.Technician,
                    MaintenanceTask = model.MaintenanceTask,
                    Comments = model.Comments,
                    NTLogin = model.NTLogin,
                    Status = "NEW"
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                response.SuccessMessage = "Equipment for maintenance has been added successfully";
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                response.AddError(message);
            }

            return response;
        }
       
        public List<LocationView> GetLocation()
        {
            return _dbContext.LocationViews.ToList();
        }

        public List<LocationView> GetSubLocation1(string locationId)
        {
            return _dbContext.LocationViews.Where(x => x.ParentLocation == locationId).ToList();
        }

        public List<LocationView> GetPrimaryLocation()
        {
            return _dbContext.LocationViews.Where(x => x.ParentLocation == null).ToList();
        }

        public Models.EquipmentMaintenances.EquipmentMaintenance GetById(string id)
        {
            return _dbContext.EquipmentMaintenances.SingleOrDefault(x => x.Id == id);
        }

        public ResultNotification<string> Update(EditEquipmentMaintainanceViewModel model)
        {
            var response = new ResultNotification<string>();

            try
            {

                var equipment = _dbContext.EquipmentMaintenances.SingleOrDefault(x => x.Id == model.Id);

                equipment.ObjectId = model.ObjectId;
                equipment.ScanBarcode = model.ScanBarcode;
                equipment.PrimaryLocationId = model.PrimaryLocationId;
                equipment.SubLocationFirstId = model.SubLocationFirstId;
                equipment.SubLocationSecondId = model.SubLocationSecondId;
                equipment.DateTime = model.DateTime;
                equipment.Technician = model.Technician;
                equipment.TroubleState = model.TroubleState;
                equipment.MaintenanceTask = model.MaintenanceTask;
                equipment.Comments = model.Comments;
                equipment.Status = model.Status;

                _dbContext.SaveChanges();

                response.SuccessMessage = "Equipment for maintenance has been updated successfully";

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                response.AddError(message);
            }
            return response;
        }

        public IQueryable<EquipmentMaintenanceView> GetEquipmentsQueryable()
        {
            return _dbContext.EquipmentMaintenanceViews;
        }
    }
}
