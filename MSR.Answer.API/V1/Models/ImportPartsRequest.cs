using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class ImportPartsRequest
    {
        /// <summary>
        /// URL encoded base64 data
        /// </summary>
        /// <example>
        /// 77u/SWQsTmFtZSxQYXJ0TnVtYmVyLE9FTVBhcnROdW1iZXIsTmlja05hbWUsTWF4aW11bUN5Y2xlcw0KMzMzNyxJbXBvcnRQYXJ0LEltcG9ydE51bWJlcixPZW1JbXBvcnQsTmlja25hbWUsMTINCg==
        /// </example>
        [Required]
        public string base64Data { get; set; }
    }
}
