using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    /// Base class for an API call with a typed result
    /// </summary>
    public class AuditActionResult<T> : AuditActionResult
    {
        /// <summary>
        /// Construct AuditActionResult with no error
        /// </summary>
        public AuditActionResult() : base()
        {
        }

        /// <summary>
        /// Construct AuditActionResult with error string
        /// </summary>
        public AuditActionResult(string errorMessage) : base(errorMessage)
        {
        }

        /// <summary>
        /// Pre: we know this result has errors
        /// </summary>
        /// <param name="result"></param>
        public AuditActionResult(AuditActionResult result) : base(result.ErrorMessages.FirstOrDefault().Message)
        {
        }

        public T Object { get; set; }
    }

    /// <summary>
    /// Base class for an API call with a non-typed result
    /// </summary>
    public class AuditActionResult
    {
        /// <summary>
        /// Gets or Sets SuccessMessage
        /// </summary>
        public string SuccessMessage { get; set; }
        /// <summary>
        /// Gets or Sets ErrorMessages
        /// </summary>
        public List<ErrorMessage> ErrorMessages { get; private set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Default Constructor for AuditActionResult
        /// </summary>
        public AuditActionResult()
        {
            ErrorMessages = new List<ErrorMessage>();
        }

        /// <summary>
        /// This constructor is to create a Audit2 action result with an error message to be displayed to the user.
        /// </summary>
        /// <param name="errorMessage"></param>
        public AuditActionResult([Localizable(false)] string errorMessage)
        {
            ErrorMessages = new List<ErrorMessage>
            {
                new ErrorMessage(0, errorMessage, null, true)
            };
        }

        /// <summary>
        /// True if any error messages exist
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return ErrorMessages.Any();
            }
        }

        public bool HasValidationErrors
        {
            get
            {
                return ErrorMessages.Any(x => x.IsValidationMessage);
            }
        }

        public bool HasTheErrorNumber(int number)
        {
            return ErrorMessages.Any(x => x.Number == number);
        }

        public void AddError(int number, string message)
        {
            ErrorMessages.Add(new ErrorMessage(number, message, null, false));
        }

        public void AddError(int number, string message, Exception exception)
        {
            ErrorMessages.Add(new ErrorMessage(number, message, exception, false));
        }

        public void AddError(string message, Exception exception)
        {
            ErrorMessages.Add(new ErrorMessage(0, message, exception, false));
        }

        //Doesnt display error to client, client will see Error has occurred
        public void AddError(string message)
        {
            ErrorMessages.Add(new ErrorMessage(0, message, null, false));
        }

        /// <summary>
        /// Displays Error to the client
        /// Eg: var param2 = "err1";
        /// AddValidationMessage("Error 1 {0}", param1);
        /// AddValidationMessage("Error 1 {0} {1}", param1, param2);
        /// </summary>
        /// <param name="format"></param>
        /// <param name="paramaters"></param>
        public void AddValidationMessage(string format, params object[] paramaters)
        {
            string message = paramaters != null && paramaters.Length > 0 ? string.Format(format, paramaters) : format;
            ErrorMessages.Add(new ErrorMessage(0, message, null, true));
        }

        public void AddErrors(IList<ErrorMessage> errors)
        {
            ErrorMessages.AddRange(errors);
        }

        public string GetAllErrorMessages()
        {
            StringBuilder sb = new StringBuilder();

            foreach (ErrorMessage item in ErrorMessages)
            {
                sb.AppendLine(item.Message);
            }

            return sb.ToString();
        }

        public ICollection<ErrorMessage> GetInternalErrorMessages()
        {
            return ErrorMessages.Where(e => !e.IsValidationMessage).ToList();
        }

        public ICollection<ErrorMessage> GetValidationMessages()
        {
            return ErrorMessages.Where(e => e.IsValidationMessage).ToList();
        }
    }
}
