namespace MSR.Answer.API.V1.Models
{
    public class FileRequest
    {
        /// <summary>
        /// Name
        /// </summary>
        /// <example>NAME1598366448</example>
        public string Name { get; set; }

        /// <summary>
        /// Base 64 String, URL encoded
        /// </summary>
        public string Base64String { get; set; }

        /// <summary>
        /// Content Type
        /// </summary>
        /// <example>text/plain</example>
        public string ContentType { get; set; }
    }
}
