using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Companies;
using Msr.Models.Documents;
using Msr.Services.Documents;

namespace Msr.Services.Companies.ViewModels
{
    public class EditCompanyViewModel
    {
        public EditCompanyViewModel()
        {
            ListReferenceFiles = new List<SelectListItem>();
            ListPictureFiles = new List<SelectListItem>();
            ListLogoFiles = new List<SelectListItem>();
            ReferenceFiles = new List<string>();
            PictureFiles = new List<string>();
            LogoFiles = new List<string>();
            CompanyTypes = new List<SelectListItem>();
            HeadPeoples = new List<SelectListItem>();
            Locations = new List<SelectListItem>();
            ListParents = new List<SelectListItem>();
        }
        public string Id { get; set; }

        [Required]
        [DisplayName("Name :")]
        public string Name { get; set; }

        [DisplayName("Type :")]
        public string CoType { get; set; }

        [DisplayName("Parent :")]
        public string Parent { get; set; }

        //[Required]
        [DisplayName("Phone :")]
        public string Phone { get; set; }

        //[Required]
        [DisplayName("ANSWER Administrator (of new root company) User ID :")]
        public string NewPersonLogin { get; set; }

        //[Required]
        [DisplayName("ANSWER Administrator Password :")]
        public string NewPersonPassword { get; set; }

        //[Required]
        [DisplayName("ANSWER Administrator First Name :")]
        public string NewPersonFirstName { get; set; }

        //[Required]
        [DisplayName("ANSWER Administrator Last Name :")]
        public string NewPersonLastName { get; set; }

        //[Required]
        [DisplayName("ANSWER Administrator Email :")]
        public string NewPersonEmail { get; set; }

        [DisplayName("Location :")]
        public string Location { get; set; }

        public string LocationName { get; set; }

        public string ParentName { get; set; }

        public string ModBy { get; set; }

        public string ObjectId { get; set; }

        public string RootCoID { get; set; }

        public string NTLogin { get; set; }

        public string PictureFile { get; set; }

        public string LogoFile { get; set; }

        [DisplayName("Reference Files :")]
        public List<string> ReferenceFiles { get; set; }

        [DisplayName("Picture Files :")]
        public List<string> PictureFiles { get; set; }

        [DisplayName("Logo Files :")]
        public List<string> LogoFiles { get; set; }

        public IList<SelectListItem> ListReferenceFiles { get; set; }
        public IList<SelectListItem> ListPictureFiles { get; set; }
        public IList<SelectListItem> ListLogoFiles { get; set; }
        public IEnumerable<SelectListItem> HeadPeoples { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> ListParents { get; set; }

        [DisplayName("Head People :")]
        public string HeadPeople { get; set; }

        public IEnumerable<SelectListItem> CompanyTypes { get; set; }

        public void Setup(DocumentFilesService documentFilesService, CompanyService companyService,string ntlog)
        {
            CompanyTypes = new List<SelectListItem>
            {
               new SelectListItem
                {
                    Text = "Company",
                    Value = "COMPANY",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Department",
                    Value = "DEPARTMENT"
                }

            };

            ListReferenceFiles = documentFilesService.GetSelectedFiles(id: ObjectId, type: null, ntlogin: ntlog).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
                //Selected = true
            }).OrderBy(o => o.Text).ToList();

            ListPictureFiles = documentFilesService.GetSelectedFiles(id: ObjectId, type: "PICTURE", ntlogin: ntlog).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            ListLogoFiles = documentFilesService.GetSelectedFiles(id: ObjectId, type: "LOGO", ntlogin: ntlog).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()               
            }).OrderBy(o => o.Text).ToList();

            HeadPeoples = companyService.GetHeadPeople().ToList().Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id
            }).OrderBy(o => o.Text).ToList();

            Locations = companyService.GetLocationsQueryable().Where(x => x.Status == "APPROVED").ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            }).OrderBy(o => o.Text).ToList();

            ListParents = companyService.GetCompaniesQueryable().Where(x=>x.Status =="APPROVED").Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            }).OrderBy(o => o.Text).ToList();
        }
       
    }
}

