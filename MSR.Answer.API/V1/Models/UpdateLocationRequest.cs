using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    ///
    /// </summary>
    public class UpdateLocationRequest
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public int LocationId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string InternalAddress { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string InvoiceClass { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? TimeZoneId { get; set; }
    }
}
