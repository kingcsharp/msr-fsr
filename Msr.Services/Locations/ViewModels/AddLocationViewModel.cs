using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Parts;
using Msr.Services.Files;
using System.ComponentModel.DataAnnotations.Schema;

namespace Msr.Services.Locations.ViewModels
{
   public class AddLocationViewModel
    {
        public AddLocationViewModel()
        {
            ListReferenceFiles = new List<SelectListItem>();
            ListPictureFiles = new List<SelectListItem>();
            ListReferenceTheories = new List<SelectListItem>();
            //ReferenceFiles = new List<string>();
            //PictureFiles = new List<string>();
            //ReferenceTheories = new List<string>();
            ListExternalEqualParts = new List<SelectListItem>();
            ListInternalEqualParts = new List<SelectListItem>();
            ListSpecialCustomers = new List<SelectListItem>();
            ListCustomerExceptions = new List<SelectListItem>();
        }

        public string Id { get; set; }

        public string ObjectId { get; set; }
        public string ObjId { get; set; }
       
        [Required]
        [DisplayName("Name :")]
        public string Name { get; set; }

        [DisplayName("ParentLocation :")]
        public string ParentLocation { get; set; }

        [DisplayName("ParentLocationName :")]
        public string ParentLocationName { get; set; }

        [DisplayName("Address1 :")]
        public string Address1 { get; set; }

        [DisplayName("Address2 :")]
        public string Address2 { get; set; }

        [DisplayName("FullAddress :")]
        public string FullAddress { get; set; }

        [DisplayName("City :")]
        public string City { get; set; }

        [DisplayName("State :")]
        public string State { get; set; }

        [DisplayName("Country :")]
        public string Country { get; set; }

        [DisplayName("PostalCode :")]
        public string  PostalCode { get; set; }

        [DisplayName("Region :")]
        public string Region { get; set; }

       
        [DisplayName("RegionName :")]
        public string RegionName { get; set; }

        [DisplayName("InternalAddress :")]
        public string InternalAddress { get; set; }

        [DisplayName("Drcm :")]
        public DateTime Drcm { get; set; }

        [DisplayName("ParentPath :")]
        public string ParentPath { get; set; }

        public string CompleteName { get; set; }

        [DisplayName("LockedBy :")]
        public string LockedBy { get; set; }

        [DisplayName("UnlockedBy :")]
        public string UnlockedBy { get; set; }

        [DisplayName("CreatedBy :")]
        public string CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        [DisplayName("Root :")]
        public string Root { get; set; }

        public string RevInfo { get; set; }

        [DisplayName("Status :")]
        public string Status { get; set; }

        [DisplayName("IsChildLocation :")]
        public Int16? IsChildLocation { get; set; }
        

        [DisplayName("Revision :")]
        public int? Revision { get; set; }

        [DisplayName("WfsId :")]
        public string WfsId { get; set; }

        [DisplayName("LockedByName :")]
        public string LockedByName { get; set; }

        [DisplayName("CreatingCoName :")]
        public string CreatingCoName { get; set; }



        public IList<SelectListItem> ListReferenceFiles { get; set; }

        public IList<SelectListItem> ListPictureFiles { get; set; }

        public IList<SelectListItem> ListReferenceTheories { get; set; }

        public IList<SelectListItem> ListInternalEqualParts { get; set; }

        public IList<SelectListItem> ListExternalEqualParts { get; set; }

        public IEnumerable<SelectListItem> OrderingUnits { get; set; }

        public IEnumerable<SelectListItem> ShippingWeightTypes { get; set; }

        public IEnumerable<SelectListItem> CreateProds { get; set; }

        public IEnumerable<SelectListItem> Spares { get; set; }

        public IEnumerable<SelectListItem> Consumables { get; set; }

        public IEnumerable<SelectListItem> Availabilities { get; set; }

        public IEnumerable<SelectListItem> PartsTypes { get; set; }

        public IEnumerable<SelectListItem> ProductTypes { get; set; }

        public IEnumerable<SelectListItem> ListSpecialCustomers { get; set; }

        public IEnumerable<SelectListItem> ListCustomerExceptions { get; set; }

        public IEnumerable<SelectListItem> ListSupplierCompany { get; set; }
    }
}
