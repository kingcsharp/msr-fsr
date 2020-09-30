using System;
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
    public partial class MenuGroup : IEquatable<MenuGroup>
    {
        /// <summary>
        /// Gets or Sets URL
        /// </summary>
        [DataMember(Name="url")]
        public string URL { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Info
        /// </summary>
        [DataMember(Name="info")]
        public string Info { get; set; }

        /// <summary>
        /// Gets or Sets Icon
        /// </summary>
        [DataMember(Name="icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or Sets OrderNumber
        /// </summary>
        [DataMember(Name="orderNumber")]
        public int OrderNumber { get; set; }
        ICollection<MenuItemRequest> MenuItems { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MenuGroup {\n");
            sb.Append("  URL: ").Append(URL).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Info: ").Append(Info).Append("\n");
            sb.Append("  Icon: ").Append(Icon).Append("\n");
            sb.Append("  OrderNumber: ").Append(OrderNumber).Append("\n");
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
            return obj.GetType() == GetType() && Equals((MenuGroup)obj);
        }

        /// <summary>
        /// Returns true if MenuGroup instances are equal
        /// </summary>
        /// <param name="other">Instance of MenuGroup to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MenuGroup other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    URL == other.URL ||
                    URL != null &&
                    URL.Equals(other.URL)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) &&
                (
                    Info == other.Info ||
                    Info != null &&
                    Info.Equals(other.Info)
                ) &&
                (
                    Icon == other.Icon ||
                    Icon != null &&
                    Icon.Equals(other.Icon)
                ) &&
                (
                    OrderNumber == other.OrderNumber ||
                    OrderNumber != null &&
                    OrderNumber.Equals(other.OrderNumber)
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
                    if (URL != null)
                    hashCode = hashCode * 59 + URL.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (Info != null)
                    hashCode = hashCode * 59 + Info.GetHashCode();
                    if (Icon != null)
                    hashCode = hashCode * 59 + Icon.GetHashCode();
                    if (OrderNumber != null)
                    hashCode = hashCode * 59 + OrderNumber.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(MenuGroup left, MenuGroup right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MenuGroup left, MenuGroup right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
