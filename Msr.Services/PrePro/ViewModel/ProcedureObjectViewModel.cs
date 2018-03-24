using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Msr.Models.PrePro;

namespace Msr.Services.PrePro.ViewModel
{
    public class ProcedureObjectViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Duration :")]
        public double? Qty { get; set; }

        [Display(Name = "Ordering Unit :")]
        public string QtyType { get; set; }

        [Display(Name = "Owner/Assistant :")]
        public string LaborRole { get; set; }

        public string ProcedureObjectId { get; set; }

        [Display(Name = "Object Name :")]
        public string ApprovedObjectId { get; set; }

        public string ProcedureStepId { get; set; }

        public string ProcId { get; set; }

        public string Relationship { get; set; }

        public string NTLogin { get; set; }

        public List<SelectListItem> ApprovedObjectList { get; set; }

        public List<SelectListItem> QtyTypeList { get; set; }

        public List<SelectListItem> LaborRoleList { get; set; }




        public void SetUp(PreProServices preProServices)
        {
            LaborRoleList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"LABOR_OWNER",
                    Value = "LABOR_OWNER"
                },
                new SelectListItem
                {
                    Text = @"LABOR_ASSISTANT",
                    Value = "LABOR_ASSISTANT"
                }
            };

            QtyTypeList = LookupItems.DurationType();

            ApprovedObjectList = preProServices.GetApprovedObjectList().Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();
        }

        public ProcedureObjectViewModel MapToDto(ProcedureObjectsLaborStepView model)
        {
            return new ProcedureObjectViewModel
            {
                Id = model.Id,
                LaborRole = model.LaborRole,
                ProcedureObjectId = model.ProcedureId,
                ProcedureStepId = model.StepId,
                Relationship = model.RelationShip,
                Qty = model.Qty,
                QtyType = model.QtyType,
                ApprovedObjectId = model.RoleId
            };
        }
    }
}

