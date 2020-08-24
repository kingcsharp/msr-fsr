using System;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class QuoteModel
    {
        public int Id { get; set; }
        public DateTime SubmittedDate { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int SubmittedById { get; set; }
        public virtual User SubmittedBy { get; set; }
        public string Contact { get; set; }
        public string Delivery { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string ProcessName { get; set; }
        public string Description { get; set; }
        public string Representative { get; set; }
        public string RepresentativeTitle { get; set; }
        public string RepresentativeAddress { get; set; }
        public string PartKitNo { get; set; }
        public int StatusId { get; set; }
        public virtual StatusModel Status { get; set; }
        public string QuoteJson { get; set; }
        public string CustomerRequirementJson { get; set; }
        public int? ProductId { get; set; }
        public virtual ProductModel Product { get; set; }
        public int Revision { get; set; }
        public ICollection<QuoteItemModel> QuoteItems { get; set; }
    }
}
