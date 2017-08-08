using Msr.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Msr.Services.Roles.ViewModels
{
    public class SaveRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Role Name :")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Security Clearance Level :")]
        public string SecurityLevel { get; set; }

        public string NTLogin { get; set; }

        public string ObjectId { get; set; }


        public IEnumerable<SelectListItem> SecurityLevels { get; set; }

        [Display(Name = "Sub Roles :")]
        public IEnumerable<HttpPostedFileBase> SubRoles { get; set; }

        [Display(Name = "Parent Roles :")]
        public IEnumerable<HttpPostedFileBase> ParentRoles { get; set; }

        [Display(Name = "People Assigned in This Module :")]
        public IEnumerable<HttpPostedFileBase> PeopleAssigned { get; set; }

        [Display(Name = "People Assigned in People Module as Formal Position :")]
        public IEnumerable<HttpPostedFileBase> PeoplePosAssigned { get; set; }

        public void Setup()
        {
            SecurityLevels = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "1 View What All Users Are Allowed to View",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "2 View What Managers & Below Are Allowed to View",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "3 View What Directors & Below Are Allowed to View",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "4 View What VP's & Below Are Allowed to View",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = "System",
                    Value = "5"
                }
            };

        }

        public SaveRoleViewModel MapToDto(RolesView model)
        {
            return new SaveRoleViewModel
            {
                Id = model.Id,
                Name = model.RoleName,
                SecurityLevel = model.SecurityLevel,
                ObjectId = model.ObjectId
            };
        }
    }
}
