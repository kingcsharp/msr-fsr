using System.ComponentModel.DataAnnotations;

namespace Msr.Services.Parts.ViewModels
{
    public class ProductSupplierView
    {
        [Key]
        public string Id { get; set; }
        public string RootCoId { get; set; }
        public string Name { get; set; }
    }
}
