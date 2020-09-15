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
    public class ProcedureStepModel
    {
        public ProcedureStepModel() { }
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Procedure that contains this step
        /// </summary>
        public Procedure Procedure { get; set; }

        /// <summary>
        /// Gets or Sets ProcedureId
        /// </summary>
        public int? ProcedureId { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets StepText
        /// </summary>
        public string StepText { get; set; }

        /// <summary>
        /// Gets or Sets Duration
        /// </summary>
        public double? Duration { get; set; }

        /// <summary>
        /// Gets or Sets DurationType
        /// </summary>
        public string DurationType { get; set; }

        /// <summary>
        /// Procedure Step Type
        /// </summary>
        public string ProcedureStepType { get; set; }

        /// <summary>
        /// Procedure Step Type
        /// </summary>
        public string ProcedureStepTypeId { get; set; }

        /// <summary>
        /// Gets or Sets PrintOrder
        /// </summary>
        public int? PrintOrder { get; set; }

        /// <summary>
        /// Gets or Sets PredecessorStepId
        /// </summary>
        public int? PredecessorStepId { get; set; }

        /// <summary>
        /// Gets or Sets LaborTime
        /// </summary>
        public int? LaborTime { get; set; }

        /// <summary>
        /// Gets or Sets EquipmentTime
        /// </summary>
        public int? EquipmentTime { get; set; }

        /// <summary>
        /// Gets or Sets ReplacementCost
        /// </summary>
        public double? ReplacementCost { get; set; }

        /// <summary>
        /// Gets or Sets UtilizationTime
        /// </summary>
        public float? UtilizationTime { get; set; }

        /// <summary>
        /// Gets or Sets UsefulLife
        /// </summary>
        public int? UsefulLife { get; set; }

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
            sb.Append("class ProcedureStepModel {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  ProcedureId: ").Append(ProcedureId).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  StepText: ").Append(StepText).Append("\n");
            sb.Append("  Duration: ").Append(Duration).Append("\n");
            sb.Append("  DurationType: ").Append(DurationType).Append("\n");
            sb.Append("  PrintOrder: ").Append(PrintOrder).Append("\n");
            sb.Append("  PredecessorStepId: ").Append(PredecessorStepId).Append("\n");
            sb.Append("  LaborTime: ").Append(LaborTime).Append("\n");
            sb.Append("  EquipmentTime: ").Append(EquipmentTime).Append("\n");
            sb.Append("  ReplacementCost: ").Append(ReplacementCost).Append("\n");
            sb.Append("  UtilizationTime: ").Append(UtilizationTime).Append("\n");
            sb.Append("  UsefulLife: ").Append(UsefulLife).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ProcedureStepModel)obj);
        }

        /// <summary>
        /// Returns true if ProcedureStepModel instances are equal
        /// </summary>
        /// <param name="other">Instance of ProcedureStepModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProcedureStepModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Id == other.Id ||
                    Id.Equals(other.Id)
                ) &&
                (
                    ProcedureId == other.ProcedureId ||
                    ProcedureId.Equals(other.ProcedureId)
                ) &&
                (
                    Title == other.Title ||
                    Title != null &&
                    Title.Equals(other.Title)
                ) &&
                (
                    StepText == other.StepText ||
                    StepText != null &&
                    StepText.Equals(other.StepText)
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
                    PrintOrder == other.PrintOrder ||
                    PrintOrder.Equals(other.PrintOrder)
                ) &&
                (
                    PredecessorStepId == other.PredecessorStepId ||
                    PredecessorStepId != null &&
                    PredecessorStepId.Equals(other.PredecessorStepId)
                ) &&
                (
                    LaborTime == other.LaborTime ||
                    LaborTime != null &&
                    LaborTime.Equals(other.LaborTime)
                ) &&
                (
                    EquipmentTime == other.EquipmentTime ||
                    EquipmentTime != null &&
                    EquipmentTime.Equals(other.EquipmentTime)
                ) &&
                (
                    ReplacementCost == other.ReplacementCost ||
                    ReplacementCost != null &&
                    ReplacementCost.Equals(other.ReplacementCost)
                ) &&
                (
                    UtilizationTime == other.UtilizationTime ||
                    UtilizationTime != null &&
                    UtilizationTime.Equals(other.UtilizationTime)
                ) &&
                (
                    UsefulLife == other.UsefulLife ||
                    UsefulLife != null &&
                    UsefulLife.Equals(other.UsefulLife)
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
                    hashCode = hashCode * 59 + ProcedureId.GetHashCode();
                    if (Title != null)
                    hashCode = hashCode * 59 + Title.GetHashCode();
                    if (StepText != null)
                    hashCode = hashCode * 59 + StepText.GetHashCode();
                    hashCode = hashCode * 59 + Duration.GetHashCode();
                    if (DurationType != null)
                    hashCode = hashCode * 59 + DurationType.GetHashCode();
                    hashCode = hashCode * 59 + PrintOrder.GetHashCode();
                    if (PredecessorStepId != null)
                    hashCode = hashCode * 59 + PredecessorStepId.GetHashCode();
                    if (LaborTime != null)
                    hashCode = hashCode * 59 + LaborTime.GetHashCode();
                    if (EquipmentTime != null)
                    hashCode = hashCode * 59 + EquipmentTime.GetHashCode();
                    if (ReplacementCost != null)
                    hashCode = hashCode * 59 + ReplacementCost.GetHashCode();
                    if (UtilizationTime != null)
                    hashCode = hashCode * 59 + UtilizationTime.GetHashCode();
                    if (UsefulLife != null)
                    hashCode = hashCode * 59 + UsefulLife.GetHashCode();
                    if (ReferenceFiles != null)
                    hashCode = hashCode * 59 + ReferenceFiles.GetHashCode();
                    if (Roles != null)
                    hashCode = hashCode * 59 + Roles.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ProcedureStepModel left, ProcedureStepModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProcedureStepModel left, ProcedureStepModel right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
