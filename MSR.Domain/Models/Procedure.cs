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
    public class Procedure: IEquatable<Procedure>
    {
        public Procedure() { }
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        public string Name { get; set; }
        public int ProcedureTypeId { get; set; }

        /// <summary>
        /// Gets or Sets IsRelatedToAProduct
        /// </summary>
        public bool? IsRelatedToAProduct { get; set; }

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
            return obj.GetType() == GetType() && Equals((Procedure)obj);
        }

        /// <summary>
        /// Returns true if Procedure instances are equal
        /// </summary>
        /// <param name="other">Instance of Procedure to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Procedure other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Id == other.Id ||
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
                    Revision.Equals(other.Revision)
                ) &&
                (
                    Comment == other.Comment ||
                    Comment != null &&
                    Comment.Equals(other.Comment)
                ) &&
                (
                    Duration == other.Duration ||
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
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (IsRelatedToAProduct != null)
                    hashCode = hashCode * 59 + IsRelatedToAProduct.GetHashCode();
                    if (CreatorCompany != null)
                    hashCode = hashCode * 59 + CreatorCompany.GetHashCode();
                    if (CreatedByDepartmentName != null)
                    hashCode = hashCode * 59 + CreatedByDepartmentName.GetHashCode();
                    hashCode = hashCode * 59 + Revision.GetHashCode();
                    if (Comment != null)
                    hashCode = hashCode * 59 + Comment.GetHashCode();
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

        public static bool operator ==(Procedure left, Procedure right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Procedure left, Procedure right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
