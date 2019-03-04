using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Infrastructure.Common.Constansts;
using Msr.Models.Locations;
using Msr.Services.Roles;
using Msr.Services.Roles.Procedures;

namespace Msr.Services.EquipmentMaintenances.ViewModels
{
    public class BaseEquipmentMaintainanceViewModel
    {
        public BaseEquipmentMaintainanceViewModel()
        {
            RoomEquipmentList = new List<SelectListItem>();
            MaintenanceTaskList = new List<SelectListItem>();
        }

        public int Id { get; set; }

        public string ObjectId { get; set; }

        public string RequestedById { get; set; }

        public string MaintenanceTask { get; set; }

        public string Comments { get; set; }

        public string ScanBarcode { get; set; }

        [DisplayName("Room/Equipment")]
        [Required]
        public string RoomEquipmentId { get; set; }

        [Required]
        public DateTime? DateTime { get; set; }

        [DisplayName("Trouble State")]
        public bool TroubleState { get; set; }

        public string Status { get; set; }

        public List<SelectListItem> StatusList { get; set; }

        public string AssignedToId { get; set; }

        public string NTLogin { get; set; }

        [Required]
        public DateTime? PemLastCompletedDate { get; set; }

        [Required]
        public int? FrequencyField { get; set; }

        public List<GetMyRolesResult> Roles { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<SelectListItem> RoomEquipmentList { get; set; }
        public List<SelectListItem> MaintenanceTaskList { get; set; }

        public void Setup(EquipmentMaintenanceService equipmentMaintenanceService, RoleService roleService)
        {

            Roles = roleService.GetAssignedRoles(NTLogin);


            //bpp
            //if (!string.IsNullOrWhiteSpace(RoomEquipmentId))
            //{
            //    LocationView loc = equipmentMaintenanceService.GetSingleLocation(RoomEquipmentId);
                RoomEquipmentList.AddRange(equipmentMaintenanceService.GetEquipmentRoom().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ObjectId,
                }).OrderBy(o => o.Text).ToList());
            // }

            MaintenanceTaskList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Add /Replace Media",
                    Value = "Add /Replace Media",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Cleaning",
                    Value = "Cleaning"
                },
                new SelectListItem
                {
                    Text = @"PM",
                    Value = "PM"
                },
                new SelectListItem
                {
                    Text = @"Repair",
                    Value = "Repair"
                }
            };

            StatusList = new List<SelectListItem>
            {
                new SelectListItem {Text = @"Select Status", Value = ""},
                new SelectListItem {Text = @"Requested", Value = "REQUESTED"},
                new SelectListItem {Text = @"Assigned", Value = "ASSIGNED"},
                new SelectListItem {Text = @"Completed", Value = "COMPLETED"},
                new SelectListItem {Text = @"Scheduled", Value = "SCHEDULED"},
            };

        }

        public void Read(Models.EquipmentMaintenances.EquipmentMaintenance model)
        {
            Id = model.Id;
            ScanBarcode = model.ScanBarcode;
            RoomEquipmentId = model.RoomEquipment;
            DateTime = model.DateTime;
            RequestedById = model.RequestedById;
            AssignedToId = model.AssignedToId;
            TroubleState = model.TroubleState;
            MaintenanceTask = model.MaintenanceTask;
            Comments = model.Comments;
            Status = model.Status;
            PemLastCompletedDate = model.PemLastCompletedDate;
            FrequencyField = model.FrequencyField;
            NTLogin = model.StrNTLogin;
        }
    }
}