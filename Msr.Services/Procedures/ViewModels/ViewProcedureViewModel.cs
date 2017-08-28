using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Comman;
using Msr.Models.Procedures;
using Msr.Services.ProcedureVerbs;
using Msr.Services.Roles;

namespace Msr.Services.Procedures.ViewModels
{
    public class ViewProcedureViewModel
    {
        public string Id { get; set; }

        public string ObjectId { get; set; }

        [Display(Name = "Procedure Name :")]
        public string Name { get; set; }

        public string Company { get; set; }

        public string CreatingCompany { get; set; }

        [Display(Name = "Procedure Verb :")]
        public string Verb { get; set; }

        [Display(Name = "Procedure System Task :")]
        public string SystemId { get; set; }

        [Display(Name = "Comments (notes,warning,etc) :")]
        public string Comments { get; set; }

        [Display(Name = "Security Clearance Level :")]
        public string SecurityLevel { get; set; }

        [Display(Name = "Roles To View :")]
        public List<string> Roles { get; set; }

        [Display(Name = "Show Steps In AP :")]
        public decimal? StepInAp { get; set; }

        [Display(Name = "Estimated Procedure Time :")]
        public double? Duration { get; set; }

        [Display(Name = "Estimated Duration Type :")]
        public string DurationType { get; set; }

        [Display(Name = "Send Msg On Approval :")]
        public decimal? WipMsg { get; set; }

        public string NTLogin { get; set; }

        public List<string> ReferenceFiles { get; set; }

        public List<SelectListItem> VerbList { get; set; }

        public List<SelectListItem> SystemList { get; set; }

        public List<SelectListItem> ReferenceFilesList { get; set; }

        public List<SelectListItem> SecurityLevelList { get; set; }

        public List<SelectListItem> SetpInApList { get; set; }

        public List<SelectListItem> DurationTypeList { get; set; }

        public List<SelectListItem> WipMsgList { get; set; }

        public List<SelectListItem> RolesList { get; set; }

        public void Setup(ProceduresService proceduresService, RoleService roleService, ProcedureVerbsService procedureTypesService,string ntlogin)
        {
            SystemList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = @"SYS_COMP_TEST",
                    Value = "SYS_COMP_TEST",
                },
                new SelectListItem
                {
                    Text = @"SYS_CONSUME",
                    Value = "SYS_CONSUME"
                },
                new SelectListItem
                {
                    Text = @"SYS_CREATE",
                    Value = "SYS_CREATE"
                },
                new SelectListItem
                {
                    Text = @"SYS_DNR",
                    Value = "SYS_DNR"
                },
                new SelectListItem
                {
                    Text = @"SYS_E_ACCESS",
                    Value = "SYS_E_ACCESS"
                },
                new SelectListItem
                {
                    Text = @"SYS_INSTALL",
                    Value = "SYS_INSTALL"
                },
                new SelectListItem
                {
                    Text = @"SYS_PROVIDE_AND_CONSUMED",
                    Value = "SYS_PROVIDE_AND_CONSUMED"
                },
                new SelectListItem
                {
                    Text = @"SYS_PROVIDE_AND_STAY",
                    Value = "SYS_PROVIDE_AND_STAY"
                },
                new SelectListItem
                {
                    Text = @"SYS_PROVIDE_TAKE_BACK",
                    Value = "SYS_PROVIDE_TAKE_BACK"
                },
                new SelectListItem
                {
                    Text = @"SYS_RECEIVE",
                    Value = "SYS_RECEIVE"
                },
                new SelectListItem
                {
                    Text = @"SYS_REMOVE",
                    Value = "SYS_REMOVE"
                },
                new SelectListItem
                {
                    Text = @"SYS_SEND",
                    Value = "SYS_SEND"
                },
                new SelectListItem
                {
                    Text = @"SYS_SERIALIZE",
                    Value = "SYS_SERIALIZE"
                },
                new SelectListItem
                {
                    Text = @"SYS_SHIPPING",
                    Value = "SYS_SHIPPING"
                }
            };
            SecurityLevelList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"SECURITY_LEVEL_PUBLIC",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"SECURITY_LEVEL_SOME_WHAT_CONFIDENTIAL",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = @"SECURITY_LEVEL_CONFIDENTIAL",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = @"SECURITY_LEVEL_EXTREMELY_CONFIDENTIAL",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = @"SYSTEM",
                    Value = "5"
                }
            };

            SetpInApList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Yes",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"No",
                    Value = "0"
                }

            };
            WipMsgList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"No",
                    Value = "0",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Yes",
                    Value = "1"
                }
            };

            DurationTypeList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"SYS_SECONDS",
                    Value = "TIME_SYS_SECONDS",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"SYS_MINUTES",
                    Value = "TIME_SYS_MINUTES"
                },
                new SelectListItem
                {
                    Text = @"SYS_HOURS",
                    Value = "TIME_SYS_HOURS"
                },
                new SelectListItem
                {
                    Text = @"SYS_DAYS",
                    Value = "TIME_SYS_DAYS"
                },
                new SelectListItem
                {
                    Text = @"SYS_WEEKS",
                    Value = "TIME_SYS_WEEKS"
                }
            };
            RolesList = roleService.GetActiveRoles().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObJect_Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            VerbList = procedureTypesService.GetProceduresVerbs().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();
            VerbList.Insert(0, new SelectListItem { Text = @"Select Varb", Value = "" });

            ReferenceFilesList = proceduresService.GetSelectedFiles(id: ObjectId,ntlogin:ntlogin, type: DBNull.Value.ToString(CultureInfo.InvariantCulture)).Select(x => new SelectListItem
            {
                Text = x.Value,
                Value = x.Show.ToString()
            }).OrderBy(o => o.Text).ToList();

            Roles = proceduresService.GetSelectedRoles(id: Id,ntlogin:ntlogin);
        }

        public SaveProcedureViewModel MapToDto(ProcedureView model)
        {
            return new SaveProcedureViewModel
            {
                Id = model.Id,
                ObjectId = model.ObjectId,
                Name = model.Name,
                CreatingCompany = model.CreatingCo,
                Verb = model.Verb,
                SystemId = model.SystemId,
                Comments = model.Comments,
                SecurityLevel = model.SecurityLevel,
                StepInAp = model.StepInAp,
                Duration = model.Duration,
                DurationType = model.DurationType,
                WipMsg = model.WipMsg
            };
        }
    }
}
