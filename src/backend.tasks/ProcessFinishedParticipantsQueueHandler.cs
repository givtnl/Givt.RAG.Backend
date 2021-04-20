using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using backend.business.Backers.Messages;
using backend.business.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PostmarkDotNet;

namespace backend.tasks
{
    public class ProcessFinishedParticipantsQueueHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProcessFinishedParticipantsQueueHandler> _logger;
        private readonly ApplicationSettings _applicationSettings;
        private readonly IAmazonSQS _sqsService;
        private readonly PostmarkClient _postMarkClient;

        public ProcessFinishedParticipantsQueueHandler(IConfiguration configuration, ILogger<ProcessFinishedParticipantsQueueHandler> logger, ApplicationSettings applicationSettings, IAmazonSQS sqsService)
        {
            _configuration = configuration;
            _logger = logger;
            _applicationSettings = applicationSettings;
            _sqsService = sqsService;
            _postMarkClient = new PostmarkClient(configuration["PostMark:ApiKey"]);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrWhiteSpace(_applicationSettings.ParticipantFinishedQueue))
            {
                _logger.LogWarning("No queue url found, aborting listening to messages");
                return;
            }

            _logger.LogDebug("Listening to messages");

            while (!stoppingToken.IsCancellationRequested)
            {
                var receiveMessageRequest = new ReceiveMessageRequest(_applicationSettings.ParticipantFinishedQueue)
                {
                    WaitTimeSeconds = 20
                };
                var queueResponse = await _sqsService.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);
                if (!queueResponse.Messages.Any())
                    continue;

                try
                {
                    var mailsToSend = queueResponse.Messages.Select(MapToMailMessage);
                    await _postMarkClient.SendEmailsWithTemplateAsync(mailsToSend.ToArray());

                    await _sqsService.DeleteMessageBatchAsync(
                        _applicationSettings.ParticipantFinishedQueue,
                        queueResponse.Messages
                            .Select(x => new DeleteMessageBatchRequestEntry(x.MessageId, x.ReceiptHandle)).ToList(), stoppingToken);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Failed to process messages {e}");
                }
            }
        }

        private TemplatedPostmarkMessage MapToMailMessage(Message queueMessage)
        {
            var messageContents = JsonSerializer.Deserialize<NotifyBackersMessage>(queueMessage.Body);
            if (messageContents == null)
                throw new Exception($"Message {queueMessage.MessageId} {queueMessage.Body} is invalid");

            var message = new TemplatedPostmarkMessage
            {
                TemplateAlias = _configuration["PostMark:TemplateAlias"],
                TemplateModel = messageContents,
                To = messageContents.Backer.EmailAddress,
                From = _configuration["PostMark:FromAddress"],
                TrackLinks = LinkTrackingOptions.HtmlAndText
            };
            return message;

        }
    }
}
