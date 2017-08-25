using Msr.Models.Roles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Common;
using Msr.Services.Users;
using Msr.Services.Users.Messages;

namespace Msr.Services.Roles.ViewModels
{
    public class SaveRoleViewModel
    {
        public SaveRoleViewModel()
        {
            ChildRoles = new List<string>();
            PeopleAssigned = new List<string>();
        }
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

        public void Setup(RoleService roleService, UserService userService, LoggedUserIdResult getCurrentUser,string ntlog)
        {
            SecurityLevels = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"1 View What All Users Are Allowed to View",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"2 View What Managers & Below Are Allowed to View",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = @"3 View What Directors & Below Are Allowed to View",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = @"4 View What VP's & Below Are Allowed to View",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = @"System",
                    Value = "5"
                }
            };

            ListChildRoles = roleService.GetActiveRoles().ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObJect_Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            ListPeopleAssigned = userService.GetSearchUser().Select(x => new SelectListItem
            {
                Text = x.Full_Name,
                Value = x.Obj_Id
            }).OrderBy(o => o.Text).ToList();


            ChildRoles = roleService.GetChildRoles(ObjectId,ntlogin:ntlog).Select(x => x.Value).ToList();

            PeopleAssigned = roleService.GetAssignedPeople(Id,ntlogin: ntlog).Select(x => x.Value).ToList();
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
