using System.Collections.Generic;

namespace Msr.Services.ActualParts.ViewModels
{
    public class ActualPartImportViewModel
    {
        public ActualPartImportViewModel()
        {
            Messages = new List<string>();
        }

        public string Id { get; set; }

        public string Sn { get; set; }

        public string NickName { get; set; }

        public string Owner { get; set; }

        public string PartId { get; set; }

        public string Qty { get; set; }

        public string LocationId { get; set; }

        public string ParentId { get; set; }

        public bool Processed { get; set; }

        public List<string> Messages { get; set; }

        public static List<string> GetHeaderColumns()
        {
            return new List<string> { "Id", "SN", "NickName", "Owner", "PartId", "Qty", "LocationId", "ParentId" };
        }
    }
}
