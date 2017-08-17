using Msr.Models.Documents;
using Msr.Services.Parts;
using Msr.Services.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.Documents.ViewModels
{
    public class SaveDocumentViewModel
    {
        public SaveDocumentViewModel()
        {
            ListReferenceFiles = new List<SelectListItem>();
            ListReferenceObjects = new List<SelectListItem>();
            ListReferenceTheories = new List<SelectListItem>();
            ListRoles = new List<SelectListItem>();
        }
        [Display(Name = "Document #:")]
        public string Id { get; set; }

        public string ObjectId { get; set; }

        [Display(Name = "Revision:")]
        public int? Rev { get; set; }

        [Display(Name = "Route Company:")]
        public string Company { get; set; }

        [Display(Name = "Document Name:")]
        public string Name { get; set; }

        [Display(Name = "Reference Documents:")]
        public List<string> ReferenceTheory { get; set; }

        [Display(Name = "Reference Objects:")]
        public List<string> ReferenceObject { get; set; }

        [Display(Name = "Reference Files:")]
        public List<string> ReferenceFiles { get; set; }

        [Display(Name = "Header Comments (notes,warning, etc.):")]
        public string Comments { get; set; }

        [Display(Name = "Security Clearance Level:")]
        public string ApprovalStatus { get; set; }

        [Display(Name = "Additional Roles Allowed Access:")]
        public List<string> Roles { get; set; }

        public string NTLogin { get; set; }

        public IList<SelectListItem> ListReferenceFiles { get; set; }
        public IList<SelectListItem> ListReferenceObjects { get; set; }
        public IList<SelectListItem> ListReferenceTheories { get; set; }
        public IList<SelectListItem> ListRoles { get; set; }

        public List<SelectListItem> ApprovalStatusList
        {
            get
            {
                return new List<SelectListItem>
            {
                    new SelectListItem{ Text = "1 View What All Users Are Allowed to View", Value = "1" },

                new SelectListItem
                {
                    Text = "2 View What Managers & Above Are Allowed to View",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "3 Only Directors & Above Allowed To View",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "4 View What VP's & Above Are Allowed to View",
                    Value = "4"
                }
            };
            }
        }
        public void Setup(RoleService roleService, PartsService partsService, DocumentService documentService)
        {
            ListRoles = roleService.GetUserRolesQueryable().Select(x => new SelectListItem
            {
                Text = x.RoleName,
                Value = x.ObjectId
            }).OrderBy(o => o.Text).ToList();
            //ListRoles.Insert(0, new SelectListItem { Text = "", Value = "" });
            
            ListReferenceFiles = partsService.GetSelectedFiles(id: ObjectId, type: null).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value,
            }).OrderBy(o => o.Text).ToList();

            ListReferenceObjects = documentService.GetSelectedObjects(id: Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList();

            ListReferenceTheories = documentService.GetSelectedTheories(id:Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value =x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList();

            Roles = documentService.GetSelectedRoles(id: Id).Select(x => x.RoleId).ToList();
        }

        public SaveDocumentViewModel MapToDto(DocumentView model)
        {
            return new SaveDocumentViewModel
            {
                Id = model.Id,
                ObjectId = model.ObjectId,
                Comments = model.Comments,
                Name = model.Name,
                Rev = model.Rev,
                ApprovalStatus = model.SecurityLevel,
                Company = model.CreatingCoName
            };
        }
    }
}
