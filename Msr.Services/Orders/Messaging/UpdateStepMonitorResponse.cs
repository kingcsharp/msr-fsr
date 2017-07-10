namespace Msr.Services.Orders.Messaging
{
    public class UpdateStepMonitorRequest
    {
        public string Id { get; set; }
        public string FailAction { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
        public string Target { get; set; }
        public string Tolerance { get; set; }
        public string TheSaurusId { get; set; }
        public string StrNTLogin { get; set; }
    }
}
