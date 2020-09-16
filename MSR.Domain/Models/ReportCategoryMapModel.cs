
namespace MSR.Domain.Models
{
    public class ReportCategoryMapModel
    {
        public int ReportId { get; set; }
        public int ReportCategoryId { get; set; }
        public ReportCategoryModel ReportCategory { get; set; }
    }
}