using System;
using System.Collections.Generic;
namespace MSR.Domain.Models
{
    public class PendingApprovalPopoverModel
    {
        public PendingApprovalPopoverModel()
        {
            Rows = new List<string>();
        }
        public ICollection<string> Rows { get; set; }

        public void AddRow(string name, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {
                if (oldValue == null)
                {
                    oldValue = "";
                }
                if (newValue == null)
                {
                    newValue = "";
                }
                this.Rows.Add($"{name}: {oldValue} To: {newValue}");
            }
        }

        public void AddRow(string name, DateTime? oldValue, DateTime? newValue)
        {
            string oldVal;
            string newVal;
            if (oldValue == newValue)
            {
                return;
            }

            if (!oldValue.HasValue)
            {
                oldVal = "";
            }
            else
            {
                oldVal = oldValue.Value.ToString("MM/dd/yy H:mm:ss zzz") + " UTC";

            }
            if (!newValue.HasValue)
            {
                newVal = "";
            }
            else
            {
                newVal = newValue.Value.ToString("MM/dd/yy H:mm:ss zzz") + " UTC";
            }

            this.Rows.Add($"{name}: {oldVal} To: {newVal}");
        }

        public void AddRow(string name, int oldValue, int newValue)
        {
            if (oldValue != newValue)
            {
                this.Rows.Add($"{name}: {oldValue} To: {newValue}");
            }
        }

        public void AddRow(string name, decimal oldValue, decimal newValue)
        {
            if (oldValue != newValue)
            {
                this.Rows.Add($"{name}: {oldValue} To: {newValue}");
            }
        }

        public void AddRow(string name, int? oldValue, int? newValue)
        {
            if (oldValue != newValue && newValue.HasValue)
            {
                var from = oldValue.HasValue ? "" : oldValue.Value.ToString();
                var to = newValue.HasValue ? "" : newValue.Value.ToString();
                this.Rows.Add($"{name}: {from} To: {to}");
            }
        }

        public void AddRow(string name, decimal? oldValue, decimal? newValue)
        {
            if (oldValue != newValue && newValue.HasValue)
            {
                var from = oldValue.HasValue ? "" : oldValue.Value.ToString();
                var to = newValue.HasValue ? "" : newValue.Value.ToString();
                this.Rows.Add($"{name}: {from} To: {to}");
            }
        }

        public void AddBoolRow(string propertyName, bool? oldValue, bool? newValue)
        {
            var oldVal = oldValue.HasValue ? SetBoolValue(propertyName, oldValue.Value) : "";
            var newVal = newValue.HasValue ? SetBoolValue(propertyName, newValue.Value) : "";
            if (oldVal != newVal)
            {
                this.Rows.Add($"{propertyName}: {oldVal} To: {newVal}");
            }
        }

        private static string SetBoolValue(string propertyName, bool oldValue)
        {
            return oldValue ? ("Is " + propertyName) : ("Is Not " + propertyName);
        }

    }
}
