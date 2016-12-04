
namespace Msr.Services.Orders.Messaging
{
    public class DocumentView
    {
        public string Id { get; set; }
        public string DocId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ServerPath { get; set; }
        public string ContentType { get; set; }
        public string FileArray { get; set; }
    }
}