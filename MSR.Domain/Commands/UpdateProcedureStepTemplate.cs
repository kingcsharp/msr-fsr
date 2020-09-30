using MSR.Domain.Commanding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Domain.Commands
{
    public class UpdateProcedureStepTemplate : Command
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Text, aka StepText
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or Sets SystemTaskId, aka ProcedureStepTypeId
        /// </summary>
        public int? SystemTaskId { get; set; }

        /// <summary>
        /// LaborTime
        /// </summary>
        public double? LaborTime { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceDocuments
        /// </summary>
        public List<int> ReferenceDocuments { get; set; }

        /// <summary>
        /// EquipmentTime
        /// </summary>
        public double? EquipmentTime { get; set; }

        /// <summary>
        /// Gets or Sets ReplacementCost
        /// </summary>
        public double? ReplacementCost { get; set; }

        /// <summary>
        /// Utilization Time
        /// </summary>
        public double? Utilization { get; set; }

        /// <summary>
        /// Gets or Sets UsefulLife
        /// </summary>
        public int? UsefulLife { get; set; }

        /// <summary>
        /// Role list
        /// </summary>
        public List<int> Roles { get; set; }

        /// <summary>
        /// Gets or Sets Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UpdateProcedureStepTemplate {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  ReplacementCost: ").Append(ReplacementCost).Append("\n");
            sb.Append("  Utilization: ").Append(Utilization).Append("\n");
            sb.Append("  UsefulLife: ").Append(UsefulLife).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
            sb.Append("  ReferenceDocuments: ").Append(ReferenceDocuments).Append("\n");
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
            return obj.GetType() == GetType() && Equals((UpdateProcedureStepTemplate)obj);
        }

        /// <summary>
        /// Returns true if UpdateProcedureTemplateinstances are equal
        /// </summary>
        /// <param name="other">Instance of UpdateProcedureTemplateto be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UpdateProcedureStepTemplate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Id == other.Id ||
                    Id.Equals(other.Id)
                ) &&
                (
                    Title == other.Title ||
                    Title != null &&
                    Title.Equals(other.Title)
                ) &&
                (
                    ReplacementCost == other.ReplacementCost ||
                    ReplacementCost != null &&
                    ReplacementCost.Equals(other.ReplacementCost)
                ) &&
                (
                    Utilization == other.Utilization ||
                    Utilization != null &&
                    Utilization.Equals(other.Utilization)
                ) &&
                (
                    UsefulLife == other.UsefulLife ||
                    UsefulLife != null &&
                    UsefulLife.Equals(other.UsefulLife)
                ) &&
                (
                    Text == other.Text ||
                    Text != null &&
                    Text.Equals(other.Text)
                ) &&
                (
                    Comments == other.Comments ||
                    Comments != null &&
                    Comments.Equals(other.Comments)
                ) &&
                (
                    ReferenceDocuments == other.ReferenceDocuments ||
                    ReferenceDocuments != null &&
                    ReferenceDocuments.SequenceEqual(other.ReferenceDocuments)
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
                    if (Title != null)
                    hashCode = hashCode * 59 + Title.GetHashCode();
                    if (ReplacementCost != null)
                    hashCode = hashCode * 59 + ReplacementCost.GetHashCode();
                    if (Utilization != null)
                    hashCode = hashCode * 59 + Utilization.GetHashCode();
                    if (UsefulLife != null)
                    hashCode = hashCode * 59 + UsefulLife.GetHashCode();
                    if (Text != null)
                    hashCode = hashCode * 59 + Text.GetHashCode();
                    if (Comments != null)
                    hashCode = hashCode * 59 + Comments.GetHashCode();
                    if (ReferenceDocuments != null)
                    hashCode = hashCode * 59 + ReferenceDocuments.GetHashCode();
                    if (Roles != null)
                    hashCode = hashCode * 59 + Roles.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(UpdateProcedureStepTemplate left, UpdateProcedureStepTemplate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UpdateProcedureStepTemplate left, UpdateProcedureStepTemplate right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
