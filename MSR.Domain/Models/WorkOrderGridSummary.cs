using MSR.Domain.Commanding.Enums;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class WorkOrderGridSummary
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets PurchaseId
        /// </summary>
        public int? PurchaseId { get; set; }

        /// <summary>
        /// This is a combination of: {CustomerName}-{WorkOrderId}
        /// </summary>
        /// <value>This is a combination of: {CustomerName}-{WorkOrderId}</value>
        public string WorkOrderItemNumber { get; set; }

        /// <summary>
        /// Gets or Sets CustomerName
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or Sets LocationName
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// This is the serial Number from the parent part, not the child parts
        /// </summary>
        /// <value>This is the serial Number from the parent part, not the child parts</value>
        public string SerialNumber { get; set; }

        /// <summary>
        /// This is the Purchase Order Database Id
        /// </summary>
        /// <value>This is the Purchase Order Id</value>
        public int? PurchaseOrderNumber { get; set; }

        /// <summary>
        /// This is the Purchase Order Database Id
        /// </summary>
        /// <value>This is the Purchase Order Id</value>
        public string ReferencePO { get; set; }

        /// <summary>
        /// This is from the parent part and not the child part
        /// </summary>
        /// <value>This is from the parent part and not the child part</value>
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or Sets ScheduledStartDate
        /// </summary>
        public DateTime? ScheduledStartDate { get; set; }

        /// <summary>
        /// Gets or Sets ScheduledEndDate
        /// </summary>
        public DateTime? ScheduledEndDate { get; set; }

        /// <summary>
        /// Gets or Sets ActualStartDate
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// Gets or Sets ActualEndDate
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// Gets or Sets ProductName
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or Sets ProcedureName
        /// </summary>
        public string ProcedureName { get; set; }

        /// <summary>
        /// Status of Work Order, case-sensitive string
        /// </summary>
        /// <value>['Waiting Start', 'In Progress', 'Cancelled', 'Completed']</value>
        public string Status { get; set; }

        /// <summary>
        /// If the Work Order has an NCR, this is the disposition result of the monitor
        /// </summary>
        /// <value>If the Work Order has an NCR, this is the disposition result of the monitor</value>
        public string Disposition { get; set; }

        /// <summary>
        /// Name of the current task that is active
        /// </summary>
        /// <value>Name of the current task that is active</value>
        public string CurrentActiveTaskName { get; set; }

        private decimal _percentageOfTasksCompleted;

        /// <summary>
        /// (Tasks Completed / Total Tasks) * 100
        /// </summary>
        public decimal? PercentageOfTasksCompleted { 
                get { 
                    
                    if(PercentageOfTasksCompletedDenominator == null || 
                        PercentageOfTasksCompletedNumerator == null || PercentageOfTasksCompletedDenominator == 0) { 
                        return 0;
                    } else { 
                    
                        return (decimal)PercentageOfTasksCompletedNumerator / (decimal)PercentageOfTasksCompletedDenominator;
                    }

                    
                }
          }

        /// <summary>
        /// Count of tasks completed
        /// </summary>
        public int? PercentageOfTasksCompletedNumerator { get; set; }

        /// <summary>
        /// Count of total tasks
        /// </summary>
        public int? PercentageOfTasksCompletedDenominator { get; set; }

        /// <summary>
        /// (Sum of time logged per a task / Sum of the expected duration time of all tasks) * 100
        /// </summary>
        public decimal? PercentageOfExpectedDurationTimeLogged {

            get
            {

                if (PercentageOfExpectedDurationTimeLoggedDenominator == null ||
                    PercentageOfExpectedDurationTimeLoggedNumerator == null || PercentageOfExpectedDurationTimeLoggedDenominator == 0)
                {
                    return 0;
                }
                else
                {

                    return ((decimal)PercentageOfExpectedDurationTimeLoggedNumerator /(decimal)PercentageOfExpectedDurationTimeLoggedDenominator)/100;
                }


            }


        }

        /// <summary>
        /// Sum of time logged
        /// </summary>
        public decimal? PercentageOfExpectedDurationTimeLoggedNumerator { get; set; }

        /// <summary>
        /// Sum of the expected time
        /// </summary>
        public decimal? PercentageOfExpectedDurationTimeLoggedDenominator { get; set; }

        /// <summary>
        /// Has NCR
        /// </summary>
        public bool HasNcr { get; set; }

        /// <summary>
        /// Segregation Type
        /// </summary>
        public EnumSegregationType? SegregationType { get;set;}
        public bool HasSubParts { get; set; }
        public ICollection<SubPartModel> SubParts { get; set; }
        public decimal? Price { get; set;}
        public bool CustomerLastRespondent { get; set; }
        public ICollection<WorkOrderMessageModel> WorkOrderMessages { get; set; }
        public bool MultipleProducts { get; set; }
    }
}
