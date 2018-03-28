using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Msr.Models.People;
using Msr.Services.Companies;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.Orders;

namespace Msr.Services.People.ViewModels
{
    public class EditPeopleViewModel
    {
        public EditPeopleViewModel()
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

        [DisplayName("ANSWER-User?:")]
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

        [DisplayName("Default Screen Type:")]
        public string ScreenType { get; set; }

        [DisplayName("Immediate Dept/Company:")]
        public string CompanyEditPerson { get; set; }

        [DisplayName("Head of Immediate Dept/Comany?")]
        public Int16? IsDepartmentHead { get; set; }

        [DisplayName("Official Position :")]
        public string OfficialPosition { get; set; }

        [DisplayName("Supervisor :")]
        public string BossName { get; set; }

        [DisplayName("Time Zone :")]
        public string TimeZone { get; set; }

        [DisplayName("Hire Date :")]
        public string HireDate { get; set; }

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
        public string EmailIdPrimary { get; set; }

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
        public string ObjectId { get; set; }
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
        public List<SelectListItem> ListCompanyEditPerson { get; set; }
        public IEnumerable<SelectListItem> ListOfficialPosition { get; set; }
        public IEnumerable<SelectListItem> ListBossInfo { get; set; }
        public List<DocLink> DocLinks { get; set; }
        public string ReferenceFiles { get; set; }

        public void Setup(DocumentFilesService documentFilesService, PeopleService peopleService,
            CompanyService companyService, string ntlog)
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
            ListEmailTextTypes = Commons.Lookups.LookupItems.ListEmailTextTypes();
            ListIsDepartmentHead = Commons.Lookups.LookupItems.YesNo();

            ListRealUserTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Yes",
                    Value = "ANSWER_USER",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "No",
                    Value = "NON_ANSWER_USER"
                }

            };

            ListScreenTypes = Commons.Lookups.LookupItems.ListScreenTypes();
            ListPhoneTypes = Commons.Lookups.LookupItems.PhoneTypes();
            ListEmailTypes = Commons.Lookups.LookupItems.EmailTypes();
            ListAddressTypes = Commons.Lookups.LookupItems.AddressTypes();

            string RootCoId = "2"; 
            ListTimeZones = peopleService.GetTimeZones().Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Id
            }).ToList();
            ListOfficialPosition = peopleService.GetOfficialPosition().Where(x => x.CreatingCo == RootCoId).Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                }).ToList();
            Locations = companyService.GetLocationsQueryable().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            }).OrderBy(o => o.Text).ToList();

            ListCompanyEditPerson.Add(new SelectListItem {Text = "--Select--", Value = ""});

            ListCompanyEditPerson.AddRange(peopleService.GetCompanyEditPersons()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                }));

            ListBossInfo = peopleService.GetBossInfo().Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.FullName
            }).Distinct().ToList();

            DocLinks = documentFilesService.GetDocByObjectId(ObjectId);
        }

        public void MapToDto(PeopleObjectView model)
        {

            Id = model.Id;
            ObjectId = model.ObjectId;
            FirstName = model.FirstName;
            LastName = model.LastName;
            OfficialPosition = model.PositionName;
            BossName = model.BossName;
            CompanyEditPerson = model.Company;
            HireDate = model.DateHired;

            if (!string.IsNullOrWhiteSpace(model.SystemStatus))
            {
                StatusEditPerson = model.SystemStatus.Trim();
            }
            ScreenType = model.ScreenType;           
            IsDepartmentHead = model.IsHead;
            TimeZone = model.TimeZone;
            LoginId = model.LoginId;
        }
    }
}
