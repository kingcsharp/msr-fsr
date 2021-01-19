using System;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    ///
    /// </summary>
    [DataContract]
    public partial class ErrorMessage
    {
        public ErrorMessage(int number, string message, Exception exception, bool displayToClient)
        {
            Number = number;
            Message = message;
            Exception = exception;
            IsValidationMessage = displayToClient;
        }

        /// <summary>
        /// Gets or Sets Number
        /// </summary>
        [DataMember(Name="number")]
        public int Number { get; set; }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [DataMember(Name="message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets IsValidationMessage
        /// </summary>
        [DataMember(Name="isValidationMessage")]
        public bool IsValidationMessage { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }
    }
}
