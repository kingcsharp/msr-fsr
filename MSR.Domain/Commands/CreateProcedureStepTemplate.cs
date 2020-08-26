using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSR.Domain.Commanding;
using MSR.Domain.Models;
using Newtonsoft.Json;

namespace MSR.Domain.Commands
{
    public class CreateProcedureStepTemplate : Command
    {
        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets BaseStartOnCounter
        /// </summary>
        public bool? BaseStartOnCounter { get; set; }

        /// <summary>
        /// Gets or Sets EstimatedStepDuration
        /// </summary>
        public int? EstimatedStepDuration { get; set; }

        /// <summary>
        /// Gets or Sets ReplacementCost
        /// </summary>
        public double? ReplacementCost { get; set; }

        /// <summary>
        /// Gets or Sets Utilization
        /// </summary>
        public double? Utilization { get; set; }

        /// <summary>
        /// Gets or Sets UsefulLife
        /// </summary>
        public decimal? UsefulLife { get; set; }

        /// <summary>
        /// Gets or Sets Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or Sets SystemTaskId
        /// </summary>
        public int? SystemTaskId { get; set; }

        /// <summary>
        /// Gets or Sets NumberOfQuestionsToUse
        /// </summary>
        public int? NumberOfQuestionsToUse { get; set; }

        /// <summary>
        /// Gets or Sets Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceProcedures
        /// </summary>
        public List<int?> ReferenceProcedures { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceDocuments
        /// </summary>
        public List<int?> ReferenceDocuments { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceFiles
        /// </summary>
        public List<FileModel> ReferenceFiles { get; set; }

        /// <summary>
        /// Gets or Sets Roles
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreateProcedureStepTemplate {\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  BaseStartOnCounter: ").Append(BaseStartOnCounter).Append("\n");
            sb.Append("  EstimatedStepDuration: ").Append(EstimatedStepDuration).Append("\n");
            sb.Append("  ReplacementCost: ").Append(ReplacementCost).Append("\n");
            sb.Append("  Utilization: ").Append(Utilization).Append("\n");
            sb.Append("  UsefulLife: ").Append(UsefulLife).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  NumberOfQuestionsToUse: ").Append(NumberOfQuestionsToUse).Append("\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
            sb.Append("  ReferenceProcedures: ").Append(ReferenceProcedures).Append("\n");
            sb.Append("  ReferenceDocuments: ").Append(ReferenceDocuments).Append("\n");
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
            return obj.GetType() == GetType() && Equals((CreateProcedureStepTemplate)obj);
        }

        /// <summary>
        /// Returns true if UpdateProcedureTemplateinstances are equal
        /// </summary>
        /// <param name="other">Instance of UpdateProcedureTemplateto be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreateProcedureStepTemplate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Title == other.Title ||
                    Title != null &&
                    Title.Equals(other.Title)
                ) &&
                (
                    BaseStartOnCounter == other.BaseStartOnCounter ||
                    BaseStartOnCounter != null &&
                    BaseStartOnCounter.Equals(other.BaseStartOnCounter)
                ) &&
                (
                    EstimatedStepDuration == other.EstimatedStepDuration ||
                    EstimatedStepDuration != null &&
                    EstimatedStepDuration.Equals(other.EstimatedStepDuration)
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
                    NumberOfQuestionsToUse == other.NumberOfQuestionsToUse ||
                    NumberOfQuestionsToUse != null &&
                    NumberOfQuestionsToUse.Equals(other.NumberOfQuestionsToUse)
                ) &&
                (
                    Comments == other.Comments ||
                    Comments != null &&
                    Comments.Equals(other.Comments)
                ) &&
                (
                    ReferenceProcedures == other.ReferenceProcedures ||
                    ReferenceProcedures != null &&
                    ReferenceProcedures.SequenceEqual(other.ReferenceProcedures)
                ) &&
                (
                    ReferenceDocuments == other.ReferenceDocuments ||
                    ReferenceDocuments != null &&
                    ReferenceDocuments.SequenceEqual(other.ReferenceDocuments)
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
                    if (Title != null)
                    hashCode = hashCode * 59 + Title.GetHashCode();
                    if (BaseStartOnCounter != null)
                    hashCode = hashCode * 59 + BaseStartOnCounter.GetHashCode();
                    if (EstimatedStepDuration != null)
                    hashCode = hashCode * 59 + EstimatedStepDuration.GetHashCode();
                    if (ReplacementCost != null)
                    hashCode = hashCode * 59 + ReplacementCost.GetHashCode();
                    if (Utilization != null)
                    hashCode = hashCode * 59 + Utilization.GetHashCode();
                    if (UsefulLife != null)
                    hashCode = hashCode * 59 + UsefulLife.GetHashCode();
                    if (Text != null)
                    hashCode = hashCode * 59 + Text.GetHashCode();
                    if (NumberOfQuestionsToUse != null)
                    hashCode = hashCode * 59 + NumberOfQuestionsToUse.GetHashCode();
                    if (Comments != null)
                    hashCode = hashCode * 59 + Comments.GetHashCode();
                    if (ReferenceProcedures != null)
                    hashCode = hashCode * 59 + ReferenceProcedures.GetHashCode();
                    if (ReferenceDocuments != null)
                    hashCode = hashCode * 59 + ReferenceDocuments.GetHashCode();
                    if (ReferenceFiles != null)
                    hashCode = hashCode * 59 + ReferenceFiles.GetHashCode();
                    if (Roles != null)
                    hashCode = hashCode * 59 + Roles.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(CreateProcedureStepTemplate left, CreateProcedureStepTemplate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CreateProcedureStepTemplate left, CreateProcedureStepTemplate right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
