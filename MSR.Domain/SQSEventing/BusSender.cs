using Amazon.SQS;
using MSR.Domain.SQSEventing.Abstractions;
using MSR.Domain.SQSEventing.Models;
using MSR.Domain.Models.Config;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using System;
using MSR.Domain.Exceptions;
using MSR.Domain.Commanding.Enums;

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

        public async Task<string> SendMessage(MessageEnvelope data)
        {
            var queueURL = _sQSInformation.QueueURL;
            SendMessageResponse response =
                await _handler.SendMessageAsync(new SendMessageRequest(queueURL, JsonConvert.SerializeObject(data))
                {
                    MessageGroupId = Guid.NewGuid().ToString(),
                    MessageDeduplicationId = Guid.NewGuid().ToString()
                });
            if (string.IsNullOrEmpty(response.MessageId))
            {
                throw new DomainException("Failure to submit message to queue", DomainError.InternalServerError);
            }
            return response.MessageId;
        }
    }
}
