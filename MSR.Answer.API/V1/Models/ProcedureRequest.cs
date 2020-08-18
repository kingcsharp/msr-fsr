using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    ///
    /// </summary>
    [DataContract]
    public partial class ProcedureRequest : IEquatable<ProcedureRequest>
    {
        public ProcedureRequest() { }
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name")]
        public string Name { get; set; }
        public int ProcedureTypeId { get; set; }

        /// <summary>
        /// Gets or Sets IsRelatedToAProduct
        /// </summary>
        [DataMember(Name="isRelatedToAProduct")]
        public bool? IsRelatedToAProduct { get; set; }

        /// <summary>
        /// Gets or Sets CreatorCompany
        /// </summary>
        [DataMember(Name="creatorCompany")]
        public string CreatorCompany { get; set; }

        /// <summary>
        /// Gets or Sets CreatedByDepartmentName
        /// </summary>
        [DataMember(Name="createdByDepartmentName")]
        public string CreatedByDepartmentName { get; set; }

        /// <summary>
        /// Gets or Sets Revision
        /// </summary>
        [DataMember(Name="revision")]
        public int Revision { get; set; }

        /// <summary>
        /// Gets or Sets Comment
        /// </summary>
        [DataMember(Name="comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or Sets Duration
        /// </summary>
        [DataMember(Name="duration")]
        public double Duration { get; set; }

        /// <summary>
        /// Gets or Sets DurationType
        /// </summary>
        [DataMember(Name="durationType")]
        public string DurationType { get; set; }

        /// <summary>
        /// Gets or Sets ProcedureType
        /// </summary>
        [DataMember(Name="procedureType")]
        public ProcedureTypeRequest ProcedureType { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceFiles
        /// </summary>
        [DataMember(Name="referenceFiles")]
        public List<FileRequest> ReferenceFiles { get; set; }

        /// <summary>
        /// Gets or Sets Roles
        /// </summary>
        [DataMember(Name="roles")]
        public List<RoleRequest> Roles { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Procedure {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  IsRelatedToAProduct: ").Append(IsRelatedToAProduct).Append("\n");
            sb.Append("  CreatorCompany: ").Append(CreatorCompany).Append("\n");
            sb.Append("  CreatedByDepartmentName: ").Append(CreatedByDepartmentName).Append("\n");
            sb.Append("  Revision: ").Append(Revision).Append("\n");
            sb.Append("  Comment: ").Append(Comment).Append("\n");
            sb.Append("  Duration: ").Append(Duration).Append("\n");
            sb.Append("  DurationType: ").Append(DurationType).Append("\n");
            sb.Append("  ProcedureType: ").Append(ProcedureType).Append("\n");
            sb.Append("  ReferenceFiles: ").Append(ReferenceFiles).Append("\n");
            sb.Append("  Roles: ").Append(Roles).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ProcedureRequest)obj);
        }

        /// <summary>
        /// Returns true if Procedure instances are equal
        /// </summary>
        /// <param name="other">Instance of Procedure to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProcedureRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) &&
                (
                    IsRelatedToAProduct == other.IsRelatedToAProduct ||
                    IsRelatedToAProduct != null &&
                    IsRelatedToAProduct.Equals(other.IsRelatedToAProduct)
                ) &&
                (
                    CreatorCompany == other.CreatorCompany ||
                    CreatorCompany != null &&
                    CreatorCompany.Equals(other.CreatorCompany)
                ) &&
                (
                    CreatedByDepartmentName == other.CreatedByDepartmentName ||
                    CreatedByDepartmentName != null &&
                    CreatedByDepartmentName.Equals(other.CreatedByDepartmentName)
                ) &&
                (
                    Revision == other.Revision ||
                    Revision != null &&
                    Revision.Equals(other.Revision)
                ) &&
                (
                    Comment == other.Comment ||
                    Comment != null &&
                    Comment.Equals(other.Comment)
                ) &&
                (
                    Duration == other.Duration ||
                    Duration != null &&
                    Duration.Equals(other.Duration)
                ) &&
                (
                    DurationType == other.DurationType ||
                    DurationType != null &&
                    DurationType.Equals(other.DurationType)
                ) &&
                (
                    ProcedureType == other.ProcedureType ||
                    ProcedureType != null &&
                    ProcedureType.Equals(other.ProcedureType)
                ) &&
                (
                    ReferenceFiles == other.ReferenceFiles ||
                    ReferenceFiles != null &&
                    ReferenceFiles.SequenceEqual(other.ReferenceFiles)
                ) &&
                (
                    Roles == other.Roles ||
                    Roles != null &&
                    Roles.SequenceEqual(other.Roles)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (IsRelatedToAProduct != null)
                    hashCode = hashCode * 59 + IsRelatedToAProduct.GetHashCode();
                    if (CreatorCompany != null)
                    hashCode = hashCode * 59 + CreatorCompany.GetHashCode();
                    if (CreatedByDepartmentName != null)
                    hashCode = hashCode * 59 + CreatedByDepartmentName.GetHashCode();
                    if (Revision != null)
                    hashCode = hashCode * 59 + Revision.GetHashCode();
                    if (Comment != null)
                    hashCode = hashCode * 59 + Comment.GetHashCode();
                    if (Duration != null)
                    hashCode = hashCode * 59 + Duration.GetHashCode();
                    if (DurationType != null)
                    hashCode = hashCode * 59 + DurationType.GetHashCode();
                    if (ProcedureType != null)
                    hashCode = hashCode * 59 + ProcedureType.GetHashCode();
                    if (ReferenceFiles != null)
                    hashCode = hashCode * 59 + ReferenceFiles.GetHashCode();
                    if (Roles != null)
                    hashCode = hashCode * 59 + Roles.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ProcedureRequest left, ProcedureRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProcedureRequest left, ProcedureRequest right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
