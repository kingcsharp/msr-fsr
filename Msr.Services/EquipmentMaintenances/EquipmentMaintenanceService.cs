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
                    RequestedById = model.RequestedById,
                    ApprovedById = model.ApprovedById,
                    MaintenanceTask = model.MaintenanceTask,
                    Comments = model.Comments,
                    NTLogin = model.NTLogin,
                    Status = "REQUESTED"
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                response.SuccessMessage = "Equipment for maintenance has been created successfully";
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
            return _dbContext.LocationViews.Where(x => x.ParentLocation == null).ToList();
        }

        public List<LocationView> GetSubLocation1(string locationId)
        {
            if (locationId == null)
            {
                return new List<LocationView>();
            }
            else
            {
                return _dbContext.LocationViews.Where(x => x.ParentLocation == locationId).ToList();
            }
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
                equipment.ParentLocation = model.PrimaryLocationId;
                equipment.SubLocationFirst = model.SubLocationFirstId;
                equipment.SubLocationSecond = model.SubLocationSecondId;
                equipment.RequestedById = equipment.RequestedById;
                equipment.TroubleState = model.TroubleState;
                equipment.MaintenanceTask = model.MaintenanceTask;
                equipment.DateTime = model.DateTime;
                equipment.Comments = model.Comments;
                equipment.Status = model.Status;
                equipment.StrNTLogin = model.NTLogin;
                equipment.ApprovedById = model.ApprovedById;


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
