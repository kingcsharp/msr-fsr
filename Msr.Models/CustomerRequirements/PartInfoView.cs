namespace Msr.Models.CustomerRequirements
{
    public class PartInfoView
    {
        public int Id { get; set; }
        public string ObjectId { get; set; }
        public string PartDescription { get; set; }
        public string Substrate { get; set; }
        public string CoatingSurface { get; set; }
        public string CustPartNo { get; set; }
        public string MfgPartNo { get; set; }
        public string PartsPerKit { get; set; }
        public string Quantity { get; set; }
        public string LeadTime { get; set; }
        public string Price { get; set; }
        public string Extension { get; set; }
        public string ProcedureId { get; set; }
        public string Status { get; set; }
    }
}
