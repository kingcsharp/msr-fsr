namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class ReportDashboardMap: Entity
    {
        public virtual Report Report { get; set; }
        public virtual int ReportId { get; set; }
    }
}
