using System.Collections.Generic;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class ReportDashboard: Entity
    {
        public ReportDashboard()
        {
            Reports = new HashSet<ReportDashboardMap>();
        }
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string APIEndPointURL { get; set; }
        public string ImageURL { get; set; }

        public virtual ICollection<ReportDashboardMap> Reports { get; set; }
    }
}
