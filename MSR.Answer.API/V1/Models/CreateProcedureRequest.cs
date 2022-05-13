using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    ///
    /// </summary>
    [DataContract]
    public partial class CreateProcedureRequest
    {
        [Required]
        public string Name { get; set; }

        public bool IsRelatedToAProduct { get; set; }

        [Required]
        public int ProcedureTypeId { get; set; }

        public string Comments { get; set; }

        public double? Duration { get; set; }

        [Required]
        public string DurationType { get; set; }

        public List<FileRequest> ReferenceFiles { get; set; }

        public ICollection<int> ReferenceFileIds { get; set; }
        public string TagType { get; set; }
    }
}
