using Amazon.SQS;
using MSR.Domain.SQSEventing.Abstractions;
using MSR.Domain.SQSEventing.Models;
using MSR.Domain.Models.Config;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MSR.Domain.SQSEventing
{
    public class BusSender : ISendSQSMessages
    {
        IAmazonSQS _handler;
        SQSInformation _sQSInformation;

        public BusSender(IAmazonSQS handler,SQSInformation sQSInformation)
        {
            _handler = handler;
            _sQSInformation = sQSInformation;
        }
       
        public async Task SendMessage(MessageEnvelope data)
        {
            var queueURL = (await _handler.GetQueueUrlAsync(_sQSInformation.QueueName)).QueueUrl;
            await _handler.SendMessageAsync(queueURL, JsonConvert.SerializeObject(data));
        }
    }
}
