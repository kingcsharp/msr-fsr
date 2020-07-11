using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class ProcedureStepMonitorInputType
    {
        public int InputTypeId { get; set; }
        public int MonitorTypeId { get; set; }
        public string InputTypeName { get; set; }
        public string MonitorTypeName { get; set; }
    }
    public class ProcedureStepMonitorListItem
    {
        public int ListItemId { get; set; }
        public int MonitorListId { get; set; }
        public string ItemName { get; set; }
        public string ListName { get; set; }
    }
    public class ProcedureStepMonitorDefinition
    {
        public List<ProcedureStepMonitorInputType> Types { get; set; }
        public List<ProcedureStepMonitorListItem> ListItems { get; set; }
    }
}
