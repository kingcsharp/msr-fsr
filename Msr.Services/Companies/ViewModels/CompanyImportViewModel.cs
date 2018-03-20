using System.Collections.Generic;

namespace Msr.Services.Companies.ViewModels
{
    public class CompanyImportViewModel
    {
        public CompanyImportViewModel()
        {
            Messages = new List<string>();
        }
        
        public string NewId { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipState { get; set; }

        public string ShipZip { get; set; }

        public string ShipCountry { get; set; }

        public string Phone { get; set; }

        public string SupplierId { get; set; }

        public string LinkedId { get; set; }

        public string StrNtLogin { get; set; }

        public bool Processed { get; set; }

        public List<string> Messages { get; set; }

        public static List<string> GetHeaderColumns()
        {
            return new List<string> { "Id", "Name", "Address", "City", "State", "Zip", "Country", "ShipName", "ShipAddress", "ShipCity", "ShipState", "ShipZip", "ShipCountry", "Phone", "LinkedId" };
        }
    }
}
