using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Msr.Services.Companies;
using Msr.Services.Documents;
using Msr.Services.Orders;

namespace Msr.Services.People.ViewModels
{
    public class AddPeopleViewModel
    {
        public AddPeopleViewModel()
        {
            ListReferenceFiles = new List<SelectListItem>();
            ListPictureFiles = new List<SelectListItem>();
            ListLanguages = new List<SelectListItem>();
            ListIsDepartmentHead = new List<SelectListItem>();
            ListStatusEditPerson = new List<SelectListItem>();
            ListTimeZones = new List<SelectListItem>();
            ListPhoneTypes = new List<SelectListItem>();
            ReferenceFiles = new List<string>();
            PictureFiles = new List<string>();
            Locations = new List<SelectListItem>();
            ListCompanyEditPerson = new List<SelectListItem>();
            ListOfficialPosition = new List<SelectListItem>();
            ListBossInfo = new List<SelectListItem>();
            ListEmailTypes = new List<SelectListItem>();
            ListAddressTypes = new List<SelectListItem>();
            ListEmailTextTypes = new List<SelectListItem>();
        }

        [DisplayName("Is A Real User :")]
        public string RealUser { get; set; }

        public string Id { get; set; }

        [DisplayName("Login ID :")]
        public string LoginId { get; set; }

        [Required]
        [DisplayName("First Name :")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name :")]
        public string LastName { get; set; }

        [DisplayName("Screen Type :")]
        public string ScreenType { get; set; }
        [DisplayName("Language :")]
        public string LanguageCode { get; set; }

        [DisplayName("Company Edit Person :")]
        public string CompanyEditPerson { get; set; }

        [DisplayName("Is Department Head :")]
        public string IsDepartmentHead { get; set; }

        [DisplayName("Official Position :")]
        public string OfficialPosition { get; set; }

        [DisplayName("Boss :")]
        public string BossName { get; set; }

        [DisplayName("Time Zone :")]
        public string TimeZone { get; set; }

        [DisplayName("Hire Date :")]
        public DateTime HireDate { get; set; }

        [DisplayName("Status Edit Person :")]
        public string StatusEditPerson { get; set; }

        [DisplayName("Phones :")]
        public string PrimaryPhoneNumber { get; set; }

        public string TypePrimaryPhoneNumber { get; set; }

        [DisplayName("Ext :")]
        public string ExtPrimaryPhoneNumber { get; set; }

        [DisplayName("Pin :")]
        public string PinPrimaryPhoneNumber { get; set; }

        public string SecondaryPhoneNumber { get; set; }
        public string TypeSecondaryPhoneNumber { get; set; }

        [DisplayName("Ext :")]
        public string ExtSecondaryPhoneNumber { get; set; }

        [DisplayName("Pin :")]
        public string PinSecondaryPhoneNumber { get; set; }

        [Required]
        [DisplayName("Emails :")]
        public string EmailPrimary { get; set; }

        public string EmailTypePrimary { get; set; }
        public string EmailTextTypePrimary { get; set; }

        [DisplayName("Name :")]
        public string EmailSecondary { get; set; }

        public string EmailTypeSecondary { get; set; }
        public string EmailTextTypeSecondary { get; set; }

        [DisplayName("Address :")]
        public string AddressType { get; set; }
        public string AddressLocation { get; set; }
        public string AddressTypeSecondary { get; set; }
        public string AddressLocationSecondary { get; set; }
        public string NTLogin { get; set; }
        public string Password { get; set; }
        public string ObjId { get; set; }
        public string newID { get; set; }
        [DisplayName("Reference Files :")]
        public List<string> ReferenceFiles { get; set; }

        [DisplayName("Picture Files :")]
        public List<string> PictureFiles { get; set; }

        public IList<SelectListItem> ListReferenceFiles { get; set; }
        public IList<SelectListItem> ListPictureFiles { get; set; }
        public IEnumerable<SelectListItem> ListRealUserTypes { get; set; }
        public IEnumerable<SelectListItem> ListScreenTypes { get; set; }
        public IEnumerable<SelectListItem> ListLanguages { get; set; }
        public IEnumerable<SelectListItem> ListIsDepartmentHead { get; set; }
        public IEnumerable<SelectListItem> ListStatusEditPerson { get; set; }
        public IEnumerable<SelectListItem> ListTimeZones { get; set; }
        public IEnumerable<SelectListItem> ListPhoneTypes { get; set; }
        public IEnumerable<SelectListItem> ListEmailTypes { get; set; }
        public IEnumerable<SelectListItem> ListEmailTextTypes { get; set; }
        public IEnumerable<SelectListItem> ListAddressTypes { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> ListCompanyEditPerson { get; set; }
        public IEnumerable<SelectListItem> ListOfficialPosition { get; set; }
        public IEnumerable<SelectListItem> ListBossInfo { get; set; }
        public void Setup(DocumentFilesService documentFilesService, PeopleService peopleService, CompanyService companyService)
        {
            ListStatusEditPerson = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Active",
                    Value = "Active",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Inactive",
                    Value = "Inactive"
                }

            };
            ListEmailTextTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Choose Type",
                    Value ="ChooseType",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "HTML",
                    Value = "HTML"
                },
                new SelectListItem
                {
                    Text = "Text",
                    Value = "Text"
                }

            };
            ListIsDepartmentHead = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Just A Worker",
                    Value = "0",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Is Department Head",
                    Value = "1"
                }

            };
            ListRealUserTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Answer User",
                    Value = "ANSWER_USER",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Answer Contact",
                    Value = "NON_ANSWER_USER"
                }

            };
            ListScreenTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Normal",
                    Value = "NORMAL_SCREEN",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Work Screen",
                    Value = "WORK_SCREEN"
                },
                new SelectListItem
                {
                    Text = "Shipper Screen",
                    Value = "SHIP_SCREEN",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Delivary Screen",
                    Value = "DELIVERY_SCREEN"
                },
                new SelectListItem
                {
                    Text = "Order Entry Screen",
                    Value = "ORDER_ENTRY_SCREEN",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Admin Screen",
                    Value = "ADMIN_SCREEN"
                }
            };
          
            ListPhoneTypes = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Choose Type", Value ="ChooseType",Selected = true},
                new SelectListItem{ Text = "SYS-PHONE-1", Value ="SYS-PHONE-1"},
                new SelectListItem{ Text = "SYS-PHONE-2", Value ="SYS-PHONE-2"},
                new SelectListItem{ Text = "SYS-PHONE-3", Value ="SYS-PHONE-3"},
                new SelectListItem{ Text = "SYS-PHONE-4", Value ="SYS-PHONE-4"},
                new SelectListItem{ Text = "SYS-PHONE-5", Value ="SYS-PHONE-5"},
                new SelectListItem{ Text = "SYS-PHONE-6", Value ="SYS-PHONE-6"},
                new SelectListItem{ Text = "SYS-PHONE-7", Value ="SYS-PHONE-7"},
                new SelectListItem{ Text = "SYS-PHONE-8", Value ="SYS-PHONE-8"},
                new SelectListItem{ Text = "SYS-PHONE-9", Value ="SYS-PHONE-9"},
                new SelectListItem{ Text = "SYS-PHONE-10", Value ="SYS-PHONE-10"},
                new SelectListItem{ Text = "SYS-PHONE-11", Value ="SYS-PHONE-11"},
                new SelectListItem{ Text = "SYS-PHONE-12", Value ="SYS-PHONE-12"},
                new SelectListItem{ Text = "SYS-PHONE-13", Value ="SYS-PHONE-13"},
                new SelectListItem{ Text = "SYS-PHONE-14", Value ="SYS-PHONE-14"},
                new SelectListItem{ Text = "SYS-PHONE-15", Value ="SYS-PHONE-15"}
            };
            ListEmailTypes = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Choose Type", Value ="ChooseType",Selected = true},
                new SelectListItem{ Text = "SYS-EMAIL-1", Value ="SYS-EMAIL-1"},
                new SelectListItem{ Text = "SYS-EMAIL-2", Value ="SYS-EMAIL-2"},
                new SelectListItem{ Text = "SYS-EMAIL-3", Value ="SYS-EMAIL-3"},
                new SelectListItem{ Text = "SYS-EMAIL-4", Value ="SYS-EMAIL-4"},
                new SelectListItem{ Text = "SYS-EMAIL-5", Value ="SYS-EMAIL-5"},
                new SelectListItem{ Text = "SYS-EMAIL-6", Value ="SYS-EMAIL-6"},
                new SelectListItem{ Text = "SYS-EMAIL-7", Value ="SYS-EMAIL-7"},
                new SelectListItem{ Text = "SYS-EMAIL-8", Value ="SYS-EMAIL-8"},
                new SelectListItem{ Text = "SYS-EMAIL-9", Value ="SYS-EMAIL-9"},
                new SelectListItem{ Text = "SYS-EMAIL-10", Value ="SYS-EMAIL-10"},
                new SelectListItem{ Text = "SYS-EMAIL-11", Value ="SYS-EMAIL-11"},
                new SelectListItem{ Text = "SYS-EMAIL-12", Value ="SYS-EMAIL-12"},
                new SelectListItem{ Text = "SYS-EMAIL-13", Value ="SYS-EMAIL-13"},
                new SelectListItem{ Text = "SYS-EMAIL-14", Value ="SYS-EMAIL-14"},
                new SelectListItem{ Text = "SYS-EMAIL-15", Value ="SYS-EMAIL-15"}
            };
            ListAddressTypes = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Choose Type", Value ="ChooseType",Selected = true},
                new SelectListItem{ Text = "SYS-ADDRESS-1", Value ="SYS-ADDRESS-1"},
                new SelectListItem{ Text = "SYS-ADDRESS-2", Value ="SYS-ADDRESS-2"},
                new SelectListItem{ Text = "SYS-ADDRESS-3", Value ="SYS-ADDRESS-3"},
                new SelectListItem{ Text = "SYS-ADDRESS-4", Value ="SYS-ADDRESS-4"},
                new SelectListItem{ Text = "SYS-ADDRESS-5", Value ="SYS-ADDRESS-5"},
                new SelectListItem{ Text = "SYS-ADDRESS-6", Value ="SYS-ADDRESS-6"},
                new SelectListItem{ Text = "SYS-ADDRESS-7", Value ="SYS-ADDRESS-7"},
                new SelectListItem{ Text = "SYS-ADDRESS-8", Value ="SYS-ADDRESS-8"},
                new SelectListItem{ Text = "SYS-ADDRESS-9", Value ="SYS-ADDRESS-9"},
                new SelectListItem{ Text = "SYS-ADDRESS-10", Value ="SYS-ADDRESS-10"},
                new SelectListItem{ Text = "SYS-ADDRESS-11", Value ="SYS-ADDRESS-11"},
                new SelectListItem{ Text = "SYS-ADDRESS-12", Value ="SYS-ADDRESS-12"},
                new SelectListItem{ Text = "SYS-ADDRESS-13", Value ="SYS-ADDRESS-13"},
                new SelectListItem{ Text = "SYS-ADDRESS-14", Value ="SYS-ADDRESS-14"},
                new SelectListItem{ Text = "SYS-ADDRESS-15", Value ="SYS-ADDRESS-15"}
            };
            string RootCoId = "2"; // need to find out from where it comes in case of add people
            ListLanguages = peopleService.GetLanguages().Select(x => new SelectListItem
            {
                Text = x.Language,
                Value = x.LanguageCode
            }).OrderBy(o => o.Text).ToList();
            ListTimeZones = peopleService.GetTimeZones().Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Id
            }).ToList();
            ListOfficialPosition = peopleService.GetOfficialPosition().Where(x => x.CreatingCo == RootCoId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            }).ToList();
            Locations = companyService.GetLocationsQueryable().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            }).OrderBy(o => o.Text).ToList();
            //ListCompanyEditPerson = peopleService.GetCompanyEditPersons().Where(x => x.RootCoId == RootCoId)
            ListCompanyEditPerson = peopleService.GetCompanyEditPersons()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                }).ToList();
            //ListBossInfo = peopleService.GetBossInfo().Where(x => x.RootCompany == RootCoId).Select(x => new SelectListItem
            ListBossInfo = peopleService.GetBossInfo().Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id
            }).Distinct().ToList();
        }
    }
}
