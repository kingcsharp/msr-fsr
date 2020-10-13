using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Domain.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ProductStepModel
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets ProductId
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or Sets ProcedureStepId
        /// </summary>
        public int ProcedureStepId { get; set; }

        /// <summary>
        /// Gets or Sets LaborMinutes
        /// </summary>
        public int? LaborMinutes { get; set; }

        /// <summary>
        /// Gets or Sets EquipmentMinutes
        /// </summary>
        public int? EquipmentMinutes { get; set; }

        /// <summary>
        /// Gets or Sets ReplacementCost
        /// </summary>
        public decimal? ReplacementCost { get; set; }

        /// <summary>
        /// Gets or Sets Utilization 
        /// </summary>
        public float? Utilization { get; set; }
        
        /// <summary>
        /// Gets or Sets UsefulLife
        /// </summary>
        public int? UsefulLife { get; set; }

        /// <summary>
        /// Gets or Sets EquipmentExpensePerMinute
        /// </summary>
        public decimal? EquipmentExpensePerMinute { get; set; }

        /// <summary>
        /// Gets or Sets RMAnnualRate
        /// </summary>
        public decimal? RMAnnualRate { get; set; }

        /// <summary>
        /// Gets or Sets RMPerMinuteRate
        /// </summary>
        public decimal? RMPerMinuteRate { get; set; }

        /// <summary>
        /// Product that contains this step
        /// </summary>
        public ProductModel Product { get; set; }

        /// <summary>
        /// ProcedureStep that contains this step
        /// </summary>
        public ProcedureStepModel ProcedureStep { get; set; }

    }
}
