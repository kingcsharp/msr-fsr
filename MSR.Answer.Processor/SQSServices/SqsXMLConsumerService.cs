using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MSR.Answer.Processor.SQSServices.Abstractions;
using Amazon.SQS;
using MSR.Domain.Models.Config;
using System.Net;
using MSR.Domain.SQSEventing.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding;
using MSR.Domain.SQSEventing.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using MSR.Domain.Helpers;
using MSR.Domain.Commanding.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace MSR.Answer.Processor.SQSServices
{
    public class SqsXMLConsumerService : ISqsXMLConsumerService
    {
        private readonly IAmazonSQS _sqsXmlTransmissionClient;
        private readonly ILogger<SqsXMLConsumerService> _logger;
        private readonly XMLSQSInformation _XMLSQSInformation;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventHandlers _eventHandlers;
        private string _queueURL;

        private CancellationTokenSource _tokenSource;

        public SqsXMLConsumerService(
            IAmazonSQS sqsClient,
            XMLSQSInformation XMLSQSInformation,
            IServiceProvider serviceProvider,
            IEventHandlers eventHandlers,
            ILogger<SqsXMLConsumerService> logger)
        {
            _eventHandlers = eventHandlers;
            _logger = logger;
            _sqsXmlTransmissionClient = sqsClient;
            _XMLSQSInformation = XMLSQSInformation;
            _serviceProvider = serviceProvider;
        }

        public async Task StartConsuming()
        {
            if (!IsConsuming())
            {
                try
                {
                    _tokenSource = new CancellationTokenSource();
                    _queueURL = _XMLSQSInformation.QueueURL;
                    _logger.LogInformation("Starting to Consume Xml Transmission");
                    // This must be await-ed and processed in order, because
                    // there is only one DbContext to use.
                    await ProcessAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
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
            return _tokenSource is { Token: { IsCancellationRequested: false } };
        }

        private async Task ProcessAsync()
        {
            try
            {
                while (!_tokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        var response = await _sqsXmlTransmissionClient.ReceiveMessageAsync(new ReceiveMessageRequest
                        {
                            QueueUrl = _queueURL,
                            //QueueUrl = _XMLSQSInformation.DLQueueURL,
                            WaitTimeSeconds = _XMLSQSInformation.LongPollingInSeconds,
                            AttributeNames = new List<string> { "ApproximateReceiveCount" },
                            MessageAttributeNames = new List<string> { "All" }
                        });

                        _logger.LogInformation($"Number of XML transferring messages Received: {response.Messages.Count}");
                        if (response.HttpStatusCode != HttpStatusCode.OK)
                        {
                            throw new AmazonSQSException($"Failed to GetMessagesAsync for queue {_XMLSQSInformation.QueueName}. Response: {response.HttpStatusCode}");
                        }

                        // Need to use foreach keyword here to ensure that
                        // processing stops and the entire SQS item completes before
                        // moving on to the next one.
                        foreach (var message in response.Messages)
                        {
                            //await _sqsXmlTransmissionClient.DeleteMessageAsync(_XMLSQSInformation.DLQueueURL, message.ReceiptHandle, _tokenSource.Token);
                            await ProcessMessageAsync(message, _tokenSource.Token);
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        _logger.LogWarning($"Failed to GetMessagesAsync for queue {_XMLSQSInformation.QueueName} because the task was canceled");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                //operation has been canceled but it shouldn't be propagated
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken)
        {
            try
            {
                var envelope = JsonConvert.DeserializeObject<MessageEnvelope>(message.Body);

                if (envelope?.Message is null)
                {
                    throw new Exception($"Unable to handle Message.  Message is not of Type: {typeof(MessageEnvelope)}");
                }

                HandleUserToken(envelope.TokenData);

                var messageType = _eventHandlers.GetReference(envelope.MessageType);
                var @event = JsonConvert.DeserializeObject(envelope.Message.ToString() ?? string.Empty, messageType);

                using var scope = _serviceProvider.CreateScope();
                var dispatcher = (EventDispatcher)(scope.ServiceProvider.GetService(typeof(EventDispatcher<>).MakeGenericType(messageType)));

                await _sqsXmlTransmissionClient.DeleteMessageAsync(_queueURL, message.ReceiptHandle, cancellationToken);

                await dispatcher.Dispatch((IEvent)@event, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot process message [id: {message.MessageId}, receiptHandle: {message.ReceiptHandle}, body: {message.Body}] from queue");
                await _sqsXmlTransmissionClient.DeleteMessageAsync(_queueURL, message.ReceiptHandle, cancellationToken);
                await _sqsXmlTransmissionClient.SendMessageAsync(new SendMessageRequest(_XMLSQSInformation.DLQueueURL, message.Body)
                {
                    MessageGroupId = Guid.NewGuid().ToString(),
                    MessageDeduplicationId = Guid.NewGuid().ToString()
                }, cancellationToken);
            }
        }

        private static void HandleUserToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenData = tokenHandler.ReadJwtToken(token);
            int.TryParse(tokenData.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value, out var accountId);
            string claimVal = tokenData.Claims.FirstOrDefault(c => c.Type == "ApprovalPrivileges").Value;
            var approvalPrivilegesDic = JsonConvert.DeserializeObject<Dictionary<int, int[]>>(claimVal);

            string userPrivileges = tokenData.Claims.FirstOrDefault(c => c.Type == "Privileges").Value;
            var deserializedUserPrivileges = JsonConvert.DeserializeObject<int[][]>(userPrivileges);

            CurrentUser.GetId = () => accountId;

            CurrentUser.CanApproveActivity = (EnumApprovalTables) =>
            {
                var activityToBeApproved = (int)EnumApprovalTables;

                approvalPrivilegesDic.TryGetValue(activityToBeApproved, out int[] privileges);

                return privileges?.Contains((int)EnumPrivilege.CanApprove) ?? false;
            };

            CurrentUser.CanReadActivity = (EnumApprovalTables) =>
            {
                var activityToBeApproved = (int)EnumApprovalTables;

                approvalPrivilegesDic.TryGetValue(activityToBeApproved, out int[] privileges);

                return privileges?.Contains((int)EnumPrivilege.CanRead) ?? false;
            };

            CurrentUser.HasPrivilege = (EnumMenuItem, EnumPrivilege) =>
            {
                var menuItemPrivileges = deserializedUserPrivileges[(int)EnumMenuItem];
                return Array.IndexOf(menuItemPrivileges, (int)EnumPrivilege) != -1;
            };

            CurrentUser.GetAccessToken = () => tokenData.RawData;
            CurrentUser.GetTokenString = () => token;
        }
    }
}
