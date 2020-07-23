namespace MSR.Domain.Models
{
    public class SubPartModel
    {
        public int? Id { get; set; }
        public int ParentId { get; set; }
        public int PartId { get; set; }
        public int Qty { get; set; }
    }
}
