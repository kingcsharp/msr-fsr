namespace MSR.Domain.Models
{
    public class QuickbooksFormatterModel
    {
        public InvoiceModel Invoice { get; set; }

        public string Data { get; set; }

        public string ExportFileName { get; set; }
     }
}
