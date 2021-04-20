using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
using Amazon.SQS.Model;
using backend.business.Backers.Messages;
using backend.business.Infrastructure;
using MediatR;

namespace backend.business.Backers.Commands.Notify
{
    public class NotifyBackersCommandHandler : IRequestHandler<NotifyBackersCommand>
    {

        private readonly ApplicationSettings _appSettings;
        private readonly IAmazonSQS _sqs;

        public NotifyBackersCommandHandler(ApplicationSettings appSettings, IAmazonSQS sqs)
        {
            _appSettings = appSettings;
            _sqs = sqs;
        }
        public async Task<Unit> Handle(NotifyBackersCommand request, CancellationToken cancellationToken)
        {
            var currentSequence = request.Backers;
            while (currentSequence.Any())
            {
                var batch = currentSequence.Take(10).ToList();
                currentSequence = currentSequence.Skip(10).ToList();

                await _sqs.SendMessageBatchAsync(_appSettings.ParticipantFinishedQueue, batch.Select(x =>
                    new SendMessageBatchRequestEntry
                    {
                        Id = x.Id,
                        MessageBody = JsonSerializer.Serialize(new NotifyBackersMessage
                        {
                            Participant = request.Participant,
                            Event = request.Event,
                            Backer = x
                        })
                    }).ToList(), cancellationToken);
            }
            return Unit.Value;
        }


    }
}