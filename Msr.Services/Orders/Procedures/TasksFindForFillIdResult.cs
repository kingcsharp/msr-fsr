using System.Collections.Generic;

namespace Msr.Services.Orders.Procedures
{
    public class TasksFindForFillIdResult
    {
        public string Title { get; set; }
        public string Step_Text_Html { get; set; }
        public string Step_Text_All_Html { get; set; }
        public int? Print_Order { get; set; }
        public string Step_Id { get; set; }
        public List<FillGetMonitorsForNcrResult> FillGetMonitorsForNcrResult { get; set; }
    }
}
