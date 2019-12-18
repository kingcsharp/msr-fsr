using System.Collections.Generic;

namespace Msr.Services.Parts.ViewModels
{
    public class ImportPartViewModel
    {
        public ImportPartViewModel()
        {
            Messages = new List<string>();
        }
        public string PartId { get; set; }

        public string Name { get; set; }

        public string OemPartNumber { get; set; }

        public bool Processed { get; set; }

        public List<string> Messages { get; set; }

        public static List<string> GetHeaderColumns()
        {
            return new List<string> { nameof(PartId), nameof(Name), nameof(OemPartNumber) };
        }
    }
}
