using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateCustomerRequest
    {
        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? LocationId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? PrimaryContactUserId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? SecondaryContactUserId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string CustomerNumber { get; set; }
    }
}
