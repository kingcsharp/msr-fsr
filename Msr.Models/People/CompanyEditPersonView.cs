using System.ComponentModel.DataAnnotations;

namespace Msr.Models.People
{
    public class CompanyEditPersonView
    {
        [Key]
        public string Id { get; set; }
        public string RootCoId { get; set; }
        public string Name { get; set; }
    }
}
