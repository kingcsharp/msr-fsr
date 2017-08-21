using Msr.Models.ApprovalGroups;
using Msr.Services.Roles;
using Msr.Services.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.ApprovalGroups.ViewModels
{
    public class EditApprovalGroupsViewModel
    {
        public EditApprovalGroupsViewModel()
        {
            MemberRoles = new List<string>();
            MemberPeoples = new List<string>();
            SpecialMembers = new List<string>();
            ListMemberPeoples = new List<SelectListItem>();
            ListMemberRoles = new List<SelectListItem>();
            ListSpecialMembers = new List<SelectListItem>();
        }
        public string Id { get; set; }

        [Required]
        [Display(Name = "WF Group Name :")]
        public string Name { get; set; }

        [Display(Name = "Stage Member People :")]
        public List<string> MemberPeoples { get; set; }

        [Display(Name = "Member Roles :")]
        public List<string> MemberRoles { get; set; }

        [Display(Name = "Special Members :")]
        public List<string> SpecialMembers { get; set; }

        public List<SelectListItem> ListMemberPeoples { get; set; }

        public List<SelectListItem> ListMemberRoles { get; set; }

        public List<SelectListItem> ListSpecialMembers { get; set; }

        public string NTLogin { get; set; }

        public void Setup(UserService userService, RoleService roleService, ApprovalGroupsService approvalGroupsService)
        {
            ////EXEC A_SP_ROLE_SELECT NULL, NULL, NULL,NULL, '1618',' ORDER BY NAME'
            ListMemberPeoples = userService.GetSearchUser().Select(x => new SelectListItem
            {
                Text = x.Full_Name,
                Value = x.Obj_Id
            }).OrderBy(o => o.Text).ToList();

            ListMemberRoles = roleService.GetUserRolesQueryable().Select(x => new SelectListItem
            {
                Text = x.RoleName,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();

            MemberPeoples = approvalGroupsService.GetGroupMembers(id: Id).ToList();

            MemberRoles = approvalGroupsService.GetGroupRoles(id: Id).Select(x => x.Id).ToList();

            SpecialMembers = approvalGroupsService.GetGroupSpecialMembers(id: Id).Select(x => x.Id).ToList();
        }

        public EditApprovalGroupsViewModel MapToDto(ApprovalGroupsView model)
        {
            return new EditApprovalGroupsViewModel
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
