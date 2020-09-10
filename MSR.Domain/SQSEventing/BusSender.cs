using Amazon.SQS;
using MSR.Domain.SQSEventing.Abstractions;
using MSR.Domain.SQSEventing.Models;
using MSR.Domain.Models.Config;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using System;

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
            //TODO: Work with Tim to figure out why permissions are not allowing to get QUEUE URL
            var queueURL = _sQSInformation.QueueURL;
            await _handler.SendMessageAsync(new SendMessageRequest(queueURL, JsonConvert.SerializeObject(data))
            {
                MessageGroupId = Guid.NewGuid().ToString(),
                MessageDeduplicationId = Guid.NewGuid().ToString()
            });
        }
    }
}
