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
using MSR.Domain.Abstractions.Services;

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
        private GeneralInformation _processorConfig;
        private string _queueURL;

        private CancellationTokenSource _tokenSource;
        private IMessageHubClient _messageHub;

        public SqsConsumerService(
            IAmazonSQS sqsClient,
            SQSInformation sQSInformation,
            GeneralInformation processorConfig,
            ICommandDispatcher dispatcher,
            IServiceProvider serviceProvider,
            IEventHandlers eventHandlers,
            IMessageHubClient messageHub,
            ILogger<SqsConsumerService> logger)
        {
            _eventHandlers = eventHandlers;
            _logger = logger;
            _sqsClient = sqsClient;
            _sQSInformation = sQSInformation;
            _dispatcher = dispatcher;
            _serviceProvider = serviceProvider;
            _messageHub = messageHub;
            _processorConfig = processorConfig;

        }

        public async Task StartConsuming()
        {
            if (!IsConsuming())
            {
                try
                {
                    _tokenSource = new CancellationTokenSource();
                    _queueURL = _sQSInformation.QueueURL;
                    _logger.LogInformation("Starting to Consume");
                    // This must be await-ed and processed in order, because
                    // there is only one DbContext to use.
                    await ProcessAsync();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                    throw;
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
            return _tokenSource != null && !_tokenSource.Token.IsCancellationRequested;
        }

        private async Task ProcessAsync()
        {
            try
            {
                Uri baseUri = new Uri(_processorConfig.APIURL);
                UriBuilder hubUri = new UriBuilder(baseUri.Scheme, baseUri.Host, baseUri.Port, "msg");
                await _messageHub.Connect(hubUri.ToString(), _logger);

                while (!_tokenSource.Token.IsCancellationRequested)
                {
                    _messageHub.SendNotification("ping", new MSR.Domain.Hub.Toaster() {
                        Message = "ping"
                    });
                    try
                    {
                        var response = await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
                        {
                            QueueUrl = _queueURL,
                            WaitTimeSeconds = _sQSInformation.LongPollingInSeconds,
                            AttributeNames = new List<string> { "ApproximateReceiveCount" },
                            MessageAttributeNames = new List<string> { "All" }
                        });

                        _logger.LogInformation($"Number of messages Recevied: {response.Messages.Count}");
                        if (response.HttpStatusCode != HttpStatusCode.OK)
                        {
                            throw new AmazonSQSException($"Failed to GetMessagesAsync for queue {_sQSInformation.QueueName}. Response: {response.HttpStatusCode}");
                        }

                        // Need to use foreach keyword here to ensure that
                        // processing stops and the entire SQS item completes before
                        // moving on to the next one.
                        foreach (Message x in response.Messages) {
                            await ProcessMessageAsync(x);
                        }
                    }
                    catch (TaskCanceledException e)
                    {
                        _logger.LogWarning($"Failed to GetMessagesAsync for queue {_sQSInformation.QueueName} because the task was canceled");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Failed to GetMessagesAsync for queue {_sQSInformation.QueueName}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                //operation has been canceled but it shouldn't be propagated
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
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

                HandleUserToken(envelope.TokenData);

                var messageType = _eventHandlers.GetReference(envelope.MessageType);
                var @event = JsonConvert.DeserializeObject(envelope.Message.ToString(), messageType);

                var dispatcher = (EventDispatcher)(_serviceProvider.GetService(typeof(EventDispatcher<>).MakeGenericType(messageType)));

                Task opr = dispatcher.Dispatch((IEvent)@event);
                await _sqsClient.DeleteMessageAsync(_queueURL, message.ReceiptHandle);

                // The process thread must wait for operation to complete before
                // going on to the next one, otherwise the database context
                // will complain about threading errors.
                Task.WaitAll(opr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot process message [id: {message.MessageId}, receiptHandle: {message.ReceiptHandle}, body: {message.Body}] from queue");
                await _sqsClient.DeleteMessageAsync(_queueURL, message.ReceiptHandle);
            }
        }

        private void HandleUserToken(string token)
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

                return privileges == null ? false : privileges.Contains((int)EnumPrivilege.CanApprove);
            };
            CurrentUser.CanReadActivity = (EnumApprovalTables) =>
            {
                var activityToBeApproved = (int)EnumApprovalTables;

                approvalPrivilegesDic.TryGetValue(activityToBeApproved, out int[] privileges);

                return privileges == null ? false : privileges.Contains((int)EnumPrivilege.CanRead);
            };
            CurrentUser.HasPrivilege = (EnumMenuItem, EnumPrivilege) =>
            {
                var menuItemPrivileges = deserializedUserPrivileges[(int)EnumMenuItem];
                if (Array.IndexOf(menuItemPrivileges, (int)EnumPrivilege) == -1)
                {
                    return false;
                }

                return true;
            };
        }
    }
}
