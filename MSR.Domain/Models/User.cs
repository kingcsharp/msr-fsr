using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Domain.Models
{
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or Sets IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or Sets UserRoleId
        /// </summary>
        public string UserRoleId { get; set; }

        /// <summary>
        /// Gets or Sets UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or Sets FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets LastName
        /// </summary>
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or Sets Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets SecurityStamp
        /// </summary>
        public string SecurityStamp { get; set; }
        /// <summary>
        /// Gets or Sets Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or Sets SupervisorId
        /// </summary>
        public int? SupervisorId { get; set; }

        /// <summary>
        /// Gets or Sets SupervisorName
        /// </summary>
        public string SupervisorName { get; set; }

        /// <summary>
        /// Gets or Sets LocationId
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// Gets or Sets LocationName
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or Sets IsAnswerUser
        /// </summary>
        public bool IsAnswerUser { get; set; }

        /// <summary>
        /// Gets or Sets CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or Sets LockoutEndDateUtc
        /// </summary>
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or Sets LockoutEnabled
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or Sets AccessFailedCount
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or Sets TimeZoneId
        /// </summary>
        public int TimeZoneId { get; set; }

        /// <summary>
        /// Gets or Sets LastUpdatedOn
        /// </summary>
        public DateTime LastUpdatedOn { get; set; }

        /// <summary>
        /// Gets or Sets LastUpdatedBy
        /// </summary>
        public int? LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets CreatedOn
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Roles
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class User {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  IsActive: ").Append(IsActive).Append("\n");
            sb.Append("  UserRoleId: ").Append(UserRoleId).Append("\n");
            sb.Append("  UserName: ").Append(UserName).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  SecurityStamp: ").Append(SecurityStamp).Append("\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  SupervisorId: ").Append(SupervisorId).Append("\n");
            sb.Append("  SupervisorName: ").Append(SupervisorName).Append("\n");
            sb.Append("  LocationId: ").Append(LocationId).Append("\n");
            sb.Append("  LocationName: ").Append(LocationName).Append("\n");
            sb.Append("  IsAnswerUser: ").Append(IsAnswerUser).Append("\n");
            sb.Append("  CustomerId: ").Append(CustomerId).Append("\n");
            sb.Append("  LockoutEndDateUtc: ").Append(LockoutEndDateUtc).Append("\n");
            sb.Append("  LockoutEnabled: ").Append(LockoutEnabled).Append("\n");
            sb.Append("  AccessFailedCount: ").Append(AccessFailedCount).Append("\n");
            sb.Append("  TimeZoneId: ").Append(TimeZoneId).Append("\n");
            sb.Append("  LastUpdatedOn: ").Append(LastUpdatedOn).Append("\n");
            sb.Append("  LastUpdatedBy: ").Append(LastUpdatedBy).Append("\n");
            sb.Append("  CreatedOn: ").Append(CreatedOn).Append("\n");
            sb.Append("  CreatedBy: ").Append(CreatedBy).Append("\n");
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
            return obj.GetType() == GetType() && Equals((User)obj);
        }

        /// <summary>
        /// Returns true if User instances are equal
        /// </summary>
        /// <param name="other">Instance of User to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Id == other.Id ||
                    Id.Equals(other.Id)
                ) &&
                (
                    IsActive == other.IsActive ||
                    IsActive.Equals(other.IsActive)
                ) &&
                (
                    UserRoleId == other.UserRoleId ||
                    UserRoleId != null &&
                    UserRoleId.Equals(other.UserRoleId)
                ) &&
                (
                    UserName == other.UserName ||
                    UserName != null &&
                    UserName.Equals(other.UserName)
                ) &&
                (
                    FirstName == other.FirstName ||
                    FirstName != null &&
                    FirstName.Equals(other.FirstName)
                ) &&
                (
                    LastName == other.LastName ||
                    LastName != null &&
                    LastName.Equals(other.LastName)
                ) &&
                (
                    Title == other.Title ||
                    Title != null &&
                    Title.Equals(other.Title)
                ) &&
                (
                    Email == other.Email ||
                    Email != null &&
                    Email.Equals(other.Email)
                ) &&
                (
                    SecurityStamp == other.SecurityStamp ||
                    SecurityStamp != null &&
                    SecurityStamp.Equals(other.SecurityStamp)
                ) &&
                (
                    Phone == other.Phone ||
                    Phone != null &&
                    Phone.Equals(other.Phone)
                ) &&
                (
                    SupervisorId == other.SupervisorId ||
                    SupervisorId != null &&
                    SupervisorId.Equals(other.SupervisorId)
                ) &&
                (
                    SupervisorName == other.SupervisorName ||
                    SupervisorName != null &&
                    SupervisorName.Equals(other.SupervisorName)
                ) &&
                (
                    LocationId == other.LocationId ||
                    LocationId != null &&
                    LocationId.Equals(other.LocationId)
                ) &&
                (
                    LocationName == other.LocationName ||
                    LocationName != null &&
                    LocationName.Equals(other.LocationName)
                ) &&
                (
                    IsAnswerUser == other.IsAnswerUser ||
                    IsAnswerUser.Equals(other.IsAnswerUser)
                ) &&
                (
                    CustomerId == other.CustomerId ||
                    CustomerId.Equals(other.CustomerId)
                ) &&
                (
                    LockoutEndDateUtc == other.LockoutEndDateUtc ||
                    LockoutEndDateUtc != null &&
                    LockoutEndDateUtc.Equals(other.LockoutEndDateUtc)
                ) &&
                (
                    LockoutEnabled == other.LockoutEnabled ||
                    LockoutEnabled.Equals(other.LockoutEnabled)
                ) &&
                (
                    AccessFailedCount == other.AccessFailedCount ||
                    AccessFailedCount.Equals(other.AccessFailedCount)
                ) &&
                (
                    TimeZoneId == other.TimeZoneId ||
                    TimeZoneId.Equals(other.TimeZoneId)
                ) &&
                (
                    LastUpdatedOn == other.LastUpdatedOn ||
                    LastUpdatedOn != null &&
                    LastUpdatedOn.Equals(other.LastUpdatedOn)
                ) &&
                (
                    LastUpdatedBy == other.LastUpdatedBy ||
                    LastUpdatedBy != null &&
                    LastUpdatedBy.Equals(other.LastUpdatedBy)
                ) &&
                (
                    CreatedOn == other.CreatedOn ||
                    CreatedOn != null &&
                    CreatedOn.Equals(other.CreatedOn)
                ) &&
                (
                    CreatedBy == other.CreatedBy ||
                    CreatedBy != null &&
                    CreatedBy.Equals(other.CreatedBy)
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
                    hashCode = hashCode * 59 + IsActive.GetHashCode();
                    if (UserRoleId != null)
                    hashCode = hashCode * 59 + UserRoleId.GetHashCode();
                    if (UserName != null)
                    hashCode = hashCode * 59 + UserName.GetHashCode();
                    if (FirstName != null)
                    hashCode = hashCode * 59 + FirstName.GetHashCode();
                    if (LastName != null)
                    hashCode = hashCode * 59 + LastName.GetHashCode();
                    if (Title != null)
                    hashCode = hashCode * 59 + Title.GetHashCode();
                    if (Email != null)
                    hashCode = hashCode * 59 + Email.GetHashCode();
                    if (SecurityStamp != null)
                    hashCode = hashCode * 59 + SecurityStamp.GetHashCode();
                    if (Phone != null)
                    hashCode = hashCode * 59 + Phone.GetHashCode();
                    if (SupervisorId != null)
                    hashCode = hashCode * 59 + SupervisorId.GetHashCode();
                    if (SupervisorName != null)
                    hashCode = hashCode * 59 + SupervisorName.GetHashCode();
                    hashCode = hashCode * 59 + LocationId.GetHashCode();
                    if (LocationName != null)
                    hashCode = hashCode * 59 + LocationName.GetHashCode();
                    hashCode = hashCode * 59 + IsAnswerUser.GetHashCode();
                    hashCode = hashCode * 59 + CustomerId.GetHashCode();
                    if (LockoutEndDateUtc != null)
                    hashCode = hashCode * 59 + LockoutEndDateUtc.GetHashCode();
                    hashCode = hashCode * 59 + LockoutEnabled.GetHashCode();
                    hashCode = hashCode * 59 + AccessFailedCount.GetHashCode();
                    hashCode = hashCode * 59 + TimeZoneId.GetHashCode();
                    if (LastUpdatedOn != null)
                    hashCode = hashCode * 59 + LastUpdatedOn.GetHashCode();
                    if (LastUpdatedBy != null)
                    hashCode = hashCode * 59 + LastUpdatedBy.GetHashCode();
                    if (CreatedOn != null)
                    hashCode = hashCode * 59 + CreatedOn.GetHashCode();
                    if (CreatedBy != null)
                    hashCode = hashCode * 59 + CreatedBy.GetHashCode();
                    if (Roles != null)
                    hashCode = hashCode * 59 + Roles.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
