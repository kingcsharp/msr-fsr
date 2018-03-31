using System.ComponentModel.DataAnnotations.Schema;

namespace Msr.Models.Procedures
{
    public class ProcedureApprovedView
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Creating_co_Name { get; set; }
        public string ROOT { get; set; }
        public string SEND_ID { get; set; }
        [NotMapped]
        public string RootId => ROOT;
    }
}
