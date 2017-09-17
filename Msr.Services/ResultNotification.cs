using System;
using System.Collections.Generic;
using System.Linq;

namespace Msr.Services
{
    public class ResultNotification<T>
    {
        public T Entity { get; set; }

        private readonly List<string> _errors = new List<string>();

        public string SuccessMessage { get; set; }

        public void AddError(string message)
        {
            _errors.Add(message);
        }

        public bool HasErrors()
        {
            return _errors.Any();
        }

        public string ErrorMessage()
        {
            return string.Join(",", _errors);
        }
    }
}
