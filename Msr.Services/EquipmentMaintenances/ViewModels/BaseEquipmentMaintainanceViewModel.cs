using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Msr.Services.EquipmentMaintenances.ViewModels
{
    public class BaseEquipmentMaintainanceViewModel
    {
        public BaseEquipmentMaintainanceViewModel()
        {
            PrimaryLocationList = new List<SelectListItem>();
            SubLocationFirstList = new List<SelectListItem>();
            SubLocationSecondList = new List<SelectListItem>();
            MaintenanceTaskList = new List<SelectListItem>();
        }

        public string Id { get; set; }

        public string ObjectId { get; set; }

        [Required]
        public string PrimaryLocationId { get; set; }

        [Required]
        public string Technician { get; set; }

        public bool? EquipmentTroubleState { get; set; }

        public string MaintenanceTask { get; set; }

        public string Comments { get; set; }

        public string ScanBarcode { get; set; }

        public string SubLocationFirstId { get; set; }

        public string SubLocationSecondId { get; set; }

        public DateTime? DateTime { get; set; }

        public bool TroubleState { get; set; }

        [Required]
        public string Status { get; set; }

        public List<SelectListItem> StatusList { get; set; }

        public string NTLogin { get; set; }


        public IEnumerable<SelectListItem> PrimaryLocationList { get; set; }
        public List<SelectListItem> SubLocationFirstList { get; set; }
        public List<SelectListItem> SubLocationSecondList { get; set; }
        public List<SelectListItem> MaintenanceTaskList { get; set; }

        public void Setup(EquipmentMaintenanceService equipmentMaintenanceService, string locationId)
        {
            PrimaryLocationList = equipmentMaintenanceService.GetLocation().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId,
            }).OrderBy(o => o.Text).ToList();

            SubLocationFirstList.Add(new SelectListItem { Value = "", Text = "Select a Sublocation" });

            SubLocationFirstList.AddRange(equipmentMaintenanceService.GetSubLocation1(PrimaryLocationId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId,
            }).OrderBy(o => o.Text).ToList());

            if (!string.IsNullOrWhiteSpace(SubLocationFirstId))
            {
                SubLocationSecondList.Add(new SelectListItem { Value = "", Text = "Select a Sublocation" });
                SubLocationSecondList.AddRange(equipmentMaintenanceService.GetSubLocation1(SubLocationFirstId).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ObjectId,
                }).OrderBy(o => o.Text).ToList());
            }
            
            MaintenanceTaskList = new List<SelectListItem>
            {
               new SelectListItem
                {
                    Text = "Add /Replace Media",
                    Value = "Add /Replace Media",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Cleaning",
                    Value = "Cleaning"
                },
                new SelectListItem
                {
                    Text = "PM",
                    Value = "PM"
                },
                new SelectListItem
                {
                    Text = "Repair",
                    Value = "Repair"
                }
            };

            StatusList = new List<SelectListItem>
            {
                new SelectListItem {Text = "Select Status"},
                new SelectListItem {Text = "New", Value = "NEW"},
                new SelectListItem {Text = "Processing", Value = "PROCESSING"},
                new SelectListItem {Text = "Completed", Value = "COMPLETED"}
            };

        }

        public void Read(Models.EquipmentMaintenances.EquipmentMaintenance model)
        {
            Id = model.Id;
            ScanBarcode = model.ScanBarcode;
            PrimaryLocationId = model.PrimaryLocationId;
            SubLocationFirstId = model.SubLocationFirstId;
            SubLocationSecondId = model.SubLocationSecondId;
            DateTime = model.DateTime;
            Technician = model.Technician;
            TroubleState = model.TroubleState;
            MaintenanceTask = model.MaintenanceTask;
            Comments = model.Comments;
            Status = model.Status;
        }
    }
}