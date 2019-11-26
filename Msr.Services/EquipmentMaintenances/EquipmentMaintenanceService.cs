using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Msr.Infrastructure.Common.Constansts;
using Msr.Models.EquipmentMaintenances;
using Msr.Models.Locations;
using Msr.Repositories;
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
                var entity = new Models.EquipmentMaintenances.EquipmentMaintenance
                {
                    ScanBarcode = model.ScanBarcode,
                    RoomEquipment = model.RoomEquipmentId,
                    DateTime = model.DateTime,
                    TroubleState = model.TroubleState,
                    MaintenanceTask = model.MaintenanceTask,
                    Comments = model.Comments,
                    StrNTLogin = model.NTLogin,
                    PemLastCompletedDate = model.PemLastCompletedDate,
                    FrequencyField = model.FrequencyField,
                    CreatedDate = DateTime.Now
                };

                entity.RequestedById = model.NTLogin;

                if (model.TroubleState)
                {
                    entity.MaintenanceTask = EquipmentMaintenanceTypeConstants.Repair;
                    entity.Status = EquipmentMaintenanceConstants.Requested;
                }
                else
                {
                    entity.MaintenanceTask = EquipmentMaintenanceTypeConstants.RoutineMaintenance;
                    entity.Status = EquipmentMaintenanceConstants.Assigned;
                    entity.AssignedToId = model.NTLogin;
                }

                _dbContext.EquipmentMaintenances.Add(entity);
                _dbContext.SaveChanges();

                response.SuccessMessage = "Equipment for maintenance has been created successfully";
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                response.AddError(message);
            }

            return response;
        }

        public List<LocationView> GetEquipmentRoom()
        {
            return _dbContext.LocationViews.Where(x => x.ParentLocation != null).ToList();
        }

        public Models.EquipmentMaintenances.EquipmentMaintenance GetById(int id)
        {
            return _dbContext.EquipmentMaintenances.SingleOrDefault(x => x.Id == id);
        }

        public ResultNotification<string> Update(EditEquipmentMaintainanceViewModel model)
        {
            var response = new ResultNotification<string>();

            try
            {
                var equipment = _dbContext.EquipmentMaintenances.Single(x => x.Id == model.Id);

                equipment.UpdatedDate = DateTime.Now;
                equipment.PemLastCompletedDate = model.PemLastCompletedDate;
                equipment.Comments = model.Comments;
                equipment.FrequencyField = model.FrequencyField;


                _dbContext.EquipmentMaintenances.AddOrUpdate(equipment);

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

        public ResultNotification<string> TakeOwnership(int id, string loginId)
        {
            var response = new ResultNotification<string>();

            try
            {
                var equipment = _dbContext.EquipmentMaintenances.Single(x => x.Id == id);

                equipment.AssignedToId = loginId;
                equipment.Status = EquipmentMaintenanceConstants.Assigned;
                equipment.UpdatedDate = DateTime.Now;

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

        public ResultNotification<string> MarkCompleted(int id, string loggedUserId)
        {
            var response = new ResultNotification<string>();

            try
            {
                var equipment = _dbContext.EquipmentMaintenances.Single(x => x.Id == id);


                if (equipment.MaintenanceTask == EquipmentMaintenanceTypeConstants.Repair)
                {
                    equipment.Status = EquipmentMaintenanceConstants.Completed;
                }
                else
                {
                    equipment.Status = EquipmentMaintenanceConstants.Requested;
                    equipment.PemLastCompletedDate = DateTime.Now;
                }

                equipment.UpdatedDate = DateTime.Now;

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
