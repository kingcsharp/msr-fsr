using Amazon.SQS;
using MSR.Domain.Intigration.Abstractions;
using MSR.Domain.Intigration.Models;
using MSR.Domain.Models.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Intigration
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
            await _handler.SendMessageAsync(_sQSInformation.QueueURL, JsonConvert.SerializeObject(data));
        }
    }
}
