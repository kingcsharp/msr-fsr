using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class ReportDashboardModel
    {
        public ReportDashboardModel()
        {
            Reports = new HashSet<ReportModel>();
        }
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string APIEndPointURL { get; set; }
        public string ImageURL { get; set; }

        public virtual ICollection<ReportModel> Reports { get; set; }
    }
}
