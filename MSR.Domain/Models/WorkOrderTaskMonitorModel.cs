namespace MSR.Domain.Models
{
    public class WorkOrderTaskMonitorModel
    {
        public int WorkOrderTaskId { get; set; }
        public virtual WorkOrderTaskModel WorkOrderTask { get; set; }
        public int ProcedureMonitorId { get; set; }
        public int? NumVal { get; set; }
        public string TextVal { get; set; }
        public string MultiVal { get; set; }
        public int SensorMappingId { get; set; }
        public string Comment { get; set; }
        /// <summary>
        /// This needs to be added
        /// </summary>
        /// <value>This needs to be added</value>
        [DataMember(Name="sensorValue")]
        public string SensorValue { get; set; }

        /// <summary>
        /// This needs to be added
        /// </summary>
        /// <value>This needs to be added</value>
        [DataMember(Name="sensorName")]
        public string SensorName { get; set; }
    }
}
