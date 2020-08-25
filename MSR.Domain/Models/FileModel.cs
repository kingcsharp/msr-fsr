namespace MSR.Domain.Models
{
    public class FileModel
    {
        /// <summary>
        ///
        /// </summary>
        /// <example>345</example>
        public int? FileId { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <example>123</example>
        public int? EntityId { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <example>FILE1598366448</example>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Base64String { get; set; }

        /// <summary>
        ///
        /// </summary>
        public byte[] FileContents { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <example>text/plain</example>
        public string ContentType { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <example>URL1598366448</example>
        public string FileURL { get; set; }
    }
}
