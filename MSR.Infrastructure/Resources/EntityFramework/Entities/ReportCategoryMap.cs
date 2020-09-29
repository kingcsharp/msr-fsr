using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class ReportCategoryMap: Entity
    {
        public virtual int ReportId { get; set; }

        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }

        public virtual int ReportCategoryId { get; set; }

        public virtual ReportCategory ReportCategory { get; set; }
    }
}
