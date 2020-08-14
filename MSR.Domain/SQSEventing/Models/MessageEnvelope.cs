namespace MSR.Domain.SQSEventing.Models
{
    public class MessageEnvelope
    {
        public string MessageType { get; }
        public string ContentType { get; }
        public object Message { get; }

        public MessageEnvelope(string messageType, object message, string contentType = "application/json")
        {
            MessageType = messageType;
            ContentType = contentType;
            Message = message;
        }
    }
}
