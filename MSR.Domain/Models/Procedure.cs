using MSR.Domain.Models.BaseModels;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Procedure : TrackableModel
    {
        public Procedure() { }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        public string Name { get; set; }
        public int ProcedureTypeId { get; set; }

        /// <summary>
        /// Gets or Sets IsRelatedToAProduct
        /// </summary>
        /// TODO: this isn't connected to anything.
        public bool IsRelatedToAProduct { get; set; }

        /// <summary>
        /// Gets or Sets CreatorCompany
        /// </summary>
        public string CreatorCompany { get; set; }

        /// <summary>
        /// Gets or Sets CreatedByDepartmentName
        /// </summary>
        public string CreatedByDepartmentName { get; set; }

        /// <summary>
        /// Gets or Sets Revision
        /// </summary>
        public int Revision { get; set; }

        /// <summary>
        /// Gets or Sets Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or Sets Duration
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Gets or Sets DurationType
        /// </summary>
        public string DurationType { get; set; }

        /// <summary>
        /// Gets or Sets ProcedureType
        /// </summary>
        public ProcedureType ProcedureType { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceFiles
        /// </summary>
        public List<FileModel> ReferenceFiles { get; set; }

        /// <summary>
        /// Gets or Sets Roles
        /// </summary>
        public List<Role> Roles { get; set; }
    }
}
