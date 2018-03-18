namespace Msr.Services.Orders.ViewModels
{
    public class SaveWorkItemImageViewModel
    {
        public string DocId { get; set; }
        public string OldDocId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public string SrcId { get; set; }
        public string SrcName { get; set; }
        public string SrcDesc { get; set; }
        public string SrcPath { get; set; }
        public string SrcContentType { get; set; }
        public string SrcChanged { get; set; }
        public string DocChanged { get; set; }
        public string DropSrc { get; set; }
        public string NTLogin { get; set; }
        public string TaskId { get; set; }
    }
}
