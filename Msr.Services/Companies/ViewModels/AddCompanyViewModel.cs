using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Msr.Services.Companies.ViewModels
{
    public class AddCompanyViewModel
    {
        public string ID { get; set; }

        [Required]
        [DisplayName("Name :")]
        public string Name { get; set; }

        [DisplayName("Type :")]
        public string CoType { get; set; }

        public string Parent { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [DisplayName("ANSWER Administrator (of new root company) User ID :")]
        public string NewPersonLogin { get; set; }

        [Required]
        [DisplayName("ANSWER Administrator Password :")]
        public string NewPersonPassword { get; set; }

        [Required]
        [DisplayName("ANSWER Administrator First Name :")]
        public string NewPersonFirstName { get; set; }

        [Required]
        [DisplayName("ANSWER Administrator Last Name :")]
        public string NewPersonLastName { get; set; }

        [Required]
        [DisplayName("ANSWER Administrator Email :")]
        public string NewPersonEmail { get; set; }

        public string Location { get; set; }

        public string LocationName { get; set; }

        public string ParentName { get; set; }

        public string ModBy { get; set; }

        public string ObjectId { get; set; }

        public string RootCoID { get; set; }

        public string NTLogin { get; set; }

        [DisplayName("Reference Files :")]
        public IEnumerable<HttpPostedFileBase> ReferenceFiles { get; set; }

        [DisplayName("Picture Files :")]
        public IEnumerable<HttpPostedFileBase> PictureFiles { get; set; }

        [DisplayName("Logo Files :")]
        public IEnumerable<HttpPostedFileBase> LogoFiles { get; set; }

        public IEnumerable<SelectListItem> CompanyTypes { get; set; }

        public void Setup()
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
        }
    }
}
