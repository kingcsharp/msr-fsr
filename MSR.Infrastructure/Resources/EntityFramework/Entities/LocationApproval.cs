using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(LocationApproval))]
    public partial class LocationApproval: TrackableEntity
    {
        public int LocationId { get; set; }

        public int OldId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(15)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public int? ParentId { get; set; }

        [StringLength(20)]
        public string InternalAddress { get; set; }

        [StringLength(20)]
        public string InvoiceClass { get; set; }

    }
}
