using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MSR.Answer.Processor.SQSServices.Abstractions;
using System.Collections.Concurrent;
using Amazon.SQS;
using MSR.Domain.Models.Config;
using System.Net;
using MSR.Domain.SQSEventing.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding;
using MSR.Domain.SQSEventing.Abstractions;

namespace MSR.Answer.Processor.SQSServices
{
    public class SqsConsumerService : ISqsConsumerService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly ILogger<SqsConsumerService> _logger;
        private readonly SQSInformation _sQSInformation;
        private readonly ICommandDispatcher _dispatcher;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventHandlers _eventHandlers;
        private string _queueURL;

        private CancellationTokenSource _tokenSource;

        public SqsConsumerService(
            ConcurrentDictionary<string,Type> typeHandlers, 
            IAmazonSQS sqsClient, 
            SQSInformation sQSInformation, 
            ICommandDispatcher dispatcher,
            IServiceProvider serviceProvider,
            IEventHandlers eventHandlers,
            ILogger<SqsConsumerService> logger)
        {
            _eventHandlers = eventHandlers;
            _logger = logger;
            _sqsClient = sqsClient;
            _sQSInformation = sQSInformation;
            _dispatcher = dispatcher;
            _serviceProvider = serviceProvider;

        }

        public async Task StartConsuming()
        {
            if (!IsConsuming())
            {
                _tokenSource = new CancellationTokenSource();
                _queueURL = (await _sqsClient.GetQueueUrlAsync(_sQSInformation.QueueName)).QueueUrl;
                ProcessAsync();
            }
        }

        public void StopConsuming()
        {
            if (IsConsuming())
            {
                _tokenSource.Cancel();
            }
        }

        private bool IsConsuming()
        {
            return _tokenSource != null && !_tokenSource.Token.IsCancellationRequested;
        }

        private async void ProcessAsync()
        {
            try
            {
                while (!_tokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        var response = await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
                        {
                            QueueUrl = _queueURL,
                            WaitTimeSeconds = _sQSInformation.LongPollingInSeconds,
                            AttributeNames = new List<string> { "ApproximateReceiveCount" },
                            MessageAttributeNames = new List<string> { "All" }
                        });

                        if (response.HttpStatusCode != HttpStatusCode.OK)
                        {
                            throw new AmazonSQSException($"Failed to GetMessagesAsync for queue {_sQSInformation.QueueName}. Response: {response.HttpStatusCode}");
                        }
                        response.Messages.ForEach(async x => await ProcessMessageAsync(x));

                    }
                    catch (TaskCanceledException)
                    {
                        _logger.LogWarning($"Failed to GetMessagesAsync for queue {_sQSInformation.QueueName} because the task was canceled");
                    }
                    catch (Exception)
                    {
                        _logger.LogError($"Failed to GetMessagesAsync for queue {_sQSInformation.QueueName}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                //operation has been canceled but it shouldn't be propagated
            }
        }

        private async Task ProcessMessageAsync(Message message)
        {
            try
            {
                var envelope = JsonConvert.DeserializeObject<MessageEnvelope>(message.Body);

                if (envelope is null || envelope.Message is null)
                {
                    throw new Exception($"Unable to handle Message.  Message is not of Type: {typeof(MessageEnvelope)}");
                }

                var messageType = _eventHandlers.GetReference(envelope.MessageType);
                var @event = JsonConvert.DeserializeObject(envelope.Message.ToString(), messageType);

                var dispatcher = (EventDispatcher)(_serviceProvider.GetService(typeof(EventDispatcher<>).MakeGenericType(messageType)));

                _ = dispatcher.Dispatch((IEvent)@event);
                await _sqsClient.DeleteMessageAsync(_queueURL, message.ReceiptHandle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot process message [id: {message.MessageId}, receiptHandle: {message.ReceiptHandle}, body: {message.Body}] from queue");
            }
        }
    }
}