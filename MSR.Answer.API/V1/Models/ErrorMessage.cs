using System;
using System.Text.Json.Serialization;

namespace MSR.Answer.API.V1.Models
{
    public class ErrorMessage
    {
        public int Number { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }

        /// <summary>
        /// Indicates if the message should be displayed to the user
        /// </summary>
        public bool IsValidationMessage { get; set; }

        public ErrorMessage(int number, string message, Exception exception, bool displayToClient)
        {
            Number = number;
            Message = message;
            Exception = exception;
            IsValidationMessage = displayToClient;
        }
    }
}
