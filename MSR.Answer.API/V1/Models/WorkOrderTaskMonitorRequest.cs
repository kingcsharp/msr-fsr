namespace MSR.Answer.API.V1.Models
{
    public class WorkOrderTaskMonitorRequest
    {
        /// <summary>
        ///
        /// </summary>
        public int WorkOrderTaskId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual WorkOrderTaskRequest WorkOrderTask { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ProcedureMonitorId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? NumVal { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TextVal { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string MultiVal { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SensorMappingId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Comment { get; set; }
    }
}
