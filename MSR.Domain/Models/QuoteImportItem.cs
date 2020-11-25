namespace MSR.Domain.Models
{
    public class QuoteImportItem
    {
        public int CustomerId { get; set; }
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
    }
}
