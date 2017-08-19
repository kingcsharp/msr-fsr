using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Monitor
{
   public class MonitorView
    {
        public string Id { get; set; }
        public string MonitorType { get; set; }
        public string Description { get; set; }
        public string StartSystemTask { get; set; }
        public string StartType { get; set; }
        public string StopSystemTask { get; set; }
        public string StopType { get; set; }
        public string CounterOrClock { get; set; }
        public string ClockUnit { get; set; }
        public Single? HighestThreshold { get; set; }
        public Single? HighThreshold { get; set; }
        public Single? Target { get; set; }
        public Single? LowThreshold { get; set; }
        public Single? LowestThreshold { get; set; }
        public string ShouldBe { get; set; }
        public Int16? Opinion { get; set; }
        public string TargetAnswerId { get; set; }
        public string RelatedObjectType { get; set; }
       
      
        public string RelatedObjectDescription { get; set; }

        public Int16? HideTarget { get; set; }
        public Int16? UseResult { get; set; }
        public Int16? FailStop { get; set; }
        public Int16? YesNoAnswer { get; set; }


        public string CorrectAnswerId { get; set; }
        public string TextTarget { get; set; }
        public string TaskId { get; set; }
        public string RollUpId { get; set; }
     
        public Double? NumVal { get; set; }
        public string TextVal { get; set; }
        public string MultChoiceAnswer { get; set; }

        public string PrintResult { get; set; }

        public string Comment { get; set; }
        public string CreatedBy { get; set; }



        public Byte? IsPassing { get; set; }
       
     


        public string ProcedureId { get; set; }
        public string StepId { get; set; }
        public string PeopleId { get; set; }
        public string PartId { get; set; }
        public string ObjectId { get; set; }
        public Byte? IsAuto { get; set; }
        public string MyAnswer { get; set; }
        public Double? Tolerance { get; set; }
      
        public string RelatedObjectId { get; set; }
       
        public string FailAction { get; set; }

        public string taskStatus { get; set; }
        public DateTime? TaskStopDate { get; set; }
        public string TaskProcedureId { get; set; }


        public string WorkerName { get; set; }
        public string TaskParent { get; set; }
        public Double? PrintOrder { get; set; }
        public string TargetObjectType { get; set; }

        public string TargetObject { get; set; }
        public string SkipMode { get; set; }
        public Byte? AlwaysPass { get; set; }
        public Byte? CantChange { get; set; }

        public string ActualPartsApprovedDataSys { get; set; }
        public string ActualPartsApprovedDataSerial { get; set; }
        public string ActualPartsApprovedDataNickName { get; set; }
        public string ActualPartsApprovedDataLocationName { get; set; }

        public string ActualPartsApprovedDataCurrentOwnerName { get; set; }
        public string ActualPartsApprovedDataCompnyPartNum { get; set; }
        public string ActualPartsApprovedDataPartDesc { get; set; }
        public string ActualPartsApprovedDataPartTypeName { get; set; }

        public Double? ActualPartsApprovedDataQty { get; set; }
        public string ActualPartsApprovedDataName { get; set; }
        public string ObjectsRoot { get; set; }
        

    }
}
