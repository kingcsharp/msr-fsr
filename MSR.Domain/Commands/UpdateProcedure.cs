using MSR.Domain.Commanding;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class UpdateProcedure : Command
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        /// <example>NAME1598366448</example>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets IsRelatedToAProduct
        /// </summary>
        public bool IsRelatedToAProduct { get; set; }

        /// <summary>
        /// Gets or Sets ProcedureTypeId
        /// </summary>
        /// <example>1</example>
        public int? ProcedureTypeId { get; set; }
        public int? Revision { get; set; }

        /// <summary>
        /// Gets or Sets Comments
        /// </summary>
        /// <example>A comment 1598366448</example>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or Sets RoleIds
        /// </summary>
        /// <example>[1,2,3]</example>
        public ICollection<int> RoleIds { get; set; }

        /// <summary>
        /// Gets or Sets Duration
        /// </summary>
        /// <example>5.2</example>
        public double? Duration { get; set; }

        /// <summary>
        /// Gets or Sets DurationType
        /// </summary>
        /// <example>hours</example>
        public string DurationType { get; set; }
    }
}
