using System;
using Msr.Models.Roles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Common;
using Msr.Services.Users;
using Msr.Services.Users.Messages;
using Msr.Services.Documents.ViewModels;
using Msr.Services.Documents;

namespace Msr.Services.Roles.ViewModels
{
    public class SaveRoleViewModel
    {
        public SaveRoleViewModel()
        {
            ChildRoles = new List<string>();
            PeopleAssigned = new List<string>();
            ReferenceFiles = new List<string>();
            CertificationRoleList = new List<CertificationRole>();
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

        public string WfId { get; set; }

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

        public List<CertificationRole> CertificationRoleList { get; set; }

        //Bpp Will need to change or delete
        //[Display(Name = "Start Date :")]
        //public DateTime? StartDate { get; set; }

        //[Display(Name = "End Date :")]
        //public DateTime? EndDate { get; set; }

        [Display(Name = "Training ID and Rev :")]
        public string TrainingIdRev { get; set; }

        [Display(Name = "Reference Docs :")]
        public List<string> ReferenceFiles { get; set; }

        public List<DocLink> DocLinks { get; set; }

        [AllowHtml]
        [Display(Name = "Event History and Comments :")]
        public string Comments { get; set; }

        public void Setup(DocumentFilesService documentFilesService, RoleService roleService, UserService userService, LoggedUserIdResult getCurrentUser)
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


            ListChildRoles = roleService.GetRolesList(getCurrentUser.Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            ListPeopleAssigned = roleService.GetPeopleAssigned(getCurrentUser.Id, getCurrentUser.Company).Select(x => new SelectListItem
            {
                Text = x.Full_Name,
                Value = x.Root
            }).OrderBy(o => o.Text).ToList();

            ChildRoles = roleService.GetChildRoles(Id, getCurrentUser.Id).Select(x => x.Value).ToList();

            var certificationRoles = roleService.GetAssignedWithCetificatePeople(Id, getCurrentUser.Id);

            PeopleAssigned = certificationRoles.Select(x => x.Value).ToList();
            CertificationRoleList = certificationRoles.ToList();
            //StartDate = certificationRoles.Select(x => x.StartDate).FirstOrDefault();
            //EndDate = certificationRoles.Select(x => x.EndDate).FirstOrDefault();

            DocLinks = documentFilesService.GetDocByObjectId(ObjectId);
        }

        public void Read(RolesView role)
        {
            Id = role.Id;
            WfId = role.Id;
            Name = role.RoleName;
            Comments = role.Comments;
            TrainingIdRev = role.TrainingIdRev;
            SecurityLevel = role.SecurityLevel;
            ObjectId = role.ObjectId;
        }
    }
}
