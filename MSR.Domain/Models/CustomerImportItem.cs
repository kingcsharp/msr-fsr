namespace MSR.Domain.Models
{ 
    public class CustomerImportItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? PrimaryContactUserId { get; set; }
        public int? SecondaryCntactUserId { get; set; }
        public int? LocationId { get; set; }
        public string CustomerNumber { get; set; }
    }
}
