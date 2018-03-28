using Msr.Models.ApprovalGroups;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public void Setup(ApprovalGroupsService approvalGroupsService, string ntlogin, string co)
        {
            ////EXEC A_SP_ROLE_SELECT NULL, NULL, NULL,NULL, '1618',' ORDER BY NAME'
            ListMemberPeoples = approvalGroupsService.GetApprovedMember(ntlogin, co).Select(x => new SelectListItem
            {
                Text = x.Full_Name,
                Value = x.Root
            }).OrderBy(o => o.Text).ToList();

            ListMemberRoles = approvalGroupsService.GetGroupMemberRoles(ntlogin).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            MemberPeoples = approvalGroupsService.GetGroupMembers(Id, ntlogin).ToList();

            MemberRoles = approvalGroupsService.GetGroupRoles(Id).Select(x => x.Value).ToList();

            SpecialMembers = approvalGroupsService.GetGroupSpecialMembers(Id).Select(x => x.Value).ToList();
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
