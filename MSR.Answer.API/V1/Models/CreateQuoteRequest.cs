using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    /// CreateQuoteRequest
    /// </summary>
    public class CreateQuoteRequest
    {
        /// <summary>
        /// CustomerId
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Contact
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// Delivery
        /// </summary>
        public string Delivery { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// ProcessName, a.k.a Procedure Name
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Representative
        /// </summary>
        public string Representative { get; set; }

        /// <summary>
        /// RepresentativeTitle
        /// </summary>
        public string RepresentativeTitle { get; set; }

        /// <summary>
        /// RepresentativeAddress
        /// </summary>
        public string RepresentativeAddress { get; set; }

        /// <summary>
        /// QuoteJson
        /// </summary>
        public string QuoteJson { get; set; }

        /// <summary>
        /// CustomerRequirementJson
        /// </summary>
        public string CustomerRequirementJson { get; set; }

        /// <summary>
        /// ProductId
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// PartKitNo
        /// </summary>
        public string PartKitNo { get; set; }

        /// <summary>
        /// QuoteItems
        /// </summary>
        [Required]
        public ICollection<CreateQuoteItemRequest> QuoteItems { get; set; }
    }
}
