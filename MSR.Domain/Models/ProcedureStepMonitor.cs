using Newtonsoft.Json;
using System.Text;

namespace MSR.Domain.Models
{
    public class ProcedureStepMonitor
    {
        public ProcedureStepMonitor() { }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets MonitorType
        /// </summary>
        public string MonitorType { get; set; }

        /// <summary>
        /// Gets or Sets MonitorTypeId
        /// </summary>
        public int MonitorTypeId { get; set; }

        /// <summary>
        /// Gets or Sets InputType
        /// </summary>
        public string InputType { get; set; }

        /// <summary>
        /// Gets or Sets InputType
        /// </summary>
        public int InputTypeId { get; set; }

        /// <summary>
        /// Gets or Sets ShouldBe
        /// </summary>
        public string ShouldBe { get; set; }

        /// <summary>
        /// Gets or Sets TargetValue
        /// </summary>
        public string TargetValue { get; set; }

        /// <summary>
        /// Gets or Sets FaultHandling
        /// </summary>
        public string FaultHandling { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets SendEmailNotification
        /// </summary>
        public bool? SendEmailNotification { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProcedureStepMonitor {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  InputType: ").Append(InputType).Append("\n");
            sb.Append("  ShouldBe: ").Append(ShouldBe).Append("\n");
            sb.Append("  TargetValue: ").Append(TargetValue).Append("\n");
            sb.Append("  FaultHandling: ").Append(FaultHandling).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  SendEmailNotification: ").Append(SendEmailNotification).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ProcedureStepMonitor)obj);
        }

        /// <summary>
        /// Returns true if ProcedureStepMonitor instances are equal
        /// </summary>
        /// <param name="other">Instance of ProcedureStepMonitor to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProcedureStepMonitor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Id == other.Id ||
                    Id.Equals(other.Id)
                ) &&
                (
                    InputType == other.InputType ||
                    InputType != null &&
                    InputType.Equals(other.InputType)
                ) &&
                (
                    ShouldBe == other.ShouldBe ||
                    ShouldBe != null &&
                    ShouldBe.Equals(other.ShouldBe)
                ) &&
                (
                    TargetValue == other.TargetValue ||
                    TargetValue != null &&
                    TargetValue.Equals(other.TargetValue)
                ) &&
                (
                    FaultHandling == other.FaultHandling ||
                    FaultHandling != null &&
                    FaultHandling.Equals(other.FaultHandling)
                ) &&
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
                ) &&
                (
                    SendEmailNotification == other.SendEmailNotification ||
                    SendEmailNotification != null &&
                    SendEmailNotification.Equals(other.SendEmailNotification)
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
                    if (InputType != null)
                    hashCode = hashCode * 59 + InputType.GetHashCode();
                    if (ShouldBe != null)
                    hashCode = hashCode * 59 + ShouldBe.GetHashCode();
                    if (TargetValue != null)
                    hashCode = hashCode * 59 + TargetValue.GetHashCode();
                    if (FaultHandling != null)
                    hashCode = hashCode * 59 + FaultHandling.GetHashCode();
                    if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                    if (SendEmailNotification != null)
                    hashCode = hashCode * 59 + SendEmailNotification.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ProcedureStepMonitor left, ProcedureStepMonitor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProcedureStepMonitor left, ProcedureStepMonitor right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
