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
            ListLanguages = new List<SelectListItem>();
            ListIsDepartmentHead = new List<SelectListItem>();
            ListStatusEditPerson = new List<SelectListItem>();
            ListTimeZones = new List<SelectListItem>();
            ListPhoneTypes = new List<SelectListItem>();
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

        [DisplayName("Company Update Person :")]
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
        [Required]
        public DateTime? HireDate { get; set; }

        [DisplayName("Status Update Person :")]
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
        [DisplayName("Email :")]
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
        [DisplayName("Reference Files :")]
        public string ReferenceFiles { get; set; }

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
                    Text = "Delivery Screen",
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

            ListPhoneTypes = LookupItems.PhoneTypes();
            ListEmailTypes = LookupItems.EmailTypes();
            ListAddressTypes = LookupItems.AddressTypes();

            string RootCoId = "2"; 
    
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
