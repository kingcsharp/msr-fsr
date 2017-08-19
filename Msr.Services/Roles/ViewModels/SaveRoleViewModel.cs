using Msr.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Common;
using Msr.Services.Orders;
using Msr.Services.Users;

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

        [Display(Name = "Child Role/s :")]
        public List<string> ChildRoles { get; set; }

        [Display(Name = "Parent Roles :")]
        public IEnumerable<SelectListItem> ParentRoles { get; set; }

        [Display(Name = "People Assigned in This Module :")]
        public List<string> PeopleAssigned { get; set; }

        [Display(Name = "People Assigned in People Module as Formal Position :")]
        public List<SelectFile> PeoplePosAssigned { get; set; }

        public List<SelectListItem> ListChildRoles { get; set; }

        public List<SelectListItem> ListPeopleAssigned { get; set; }

        public void Setup(RoleService roleService, UserService userService)
        {
            SecurityLevels = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"ROLES_SECURITY_LEVEL_PUBLIC",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"ROLES_SECURITY_LEVEL_SOME_WHAT_CONFIDENTIAL",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = @"ROLES_SECURITY_LEVEL_CONFIDENTIAL",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = @"ROLES_SECURITY_LEVEL_EXTREMELY_CONFIDENTIAL",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = @"ROLES_SYSTEM",
                    Value = "5"
                }
            };

            ListChildRoles = roleService.GetActiveRoles().ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObJect_Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            ListPeopleAssigned = userService.GetPeoplesQueryable().Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();

            ChildRoles = roleService.GetChildRoles(id: ObjectId).Select(x => x.Value).ToList();

            PeopleAssigned = roleService.GetAssignedPeople(id: ObjectId).Select(x => x.Value).ToList();
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
