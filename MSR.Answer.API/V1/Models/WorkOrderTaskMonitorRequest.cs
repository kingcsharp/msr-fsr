namespace MSR.Answer.API.V1.Models
{
    public class WorkOrderTaskMonitorRequest
    {
        public int WorkOrderTaskId { get; set; }
        public virtual WorkOrderTaskRequest WorkOrderTask { get; set; }
        public int ProcedureMonitorId { get; set; }
        public int? NumVal { get; set; }
        public string TextVal { get; set; }
        public string MultiVal { get; set; }
        public int SensorMappingId { get; set; }
        public string Comment { get; set; }
    }
}
