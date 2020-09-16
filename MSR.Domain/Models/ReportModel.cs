using System.Collections.Generic;
namespace MSR.Domain.Models
{
    public class ReportModel
    {
        public ReportModel()
        {
            Categories = new HashSet<ReportCategoryMapModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string APIEndPointURL { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ReportCategoryMapModel> Categories { get; set; }
    }
}