using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using backend.business.Infrastructure;
using backend.domain;
using MediatR;

namespace backend.business.Participants.Commands.Start
{
    public class StartParticipantCommandHandler : IRequestHandler<StartParticipantCommand>
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public StartParticipantCommandHandler(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }
        public async Task<Unit> Handle(StartParticipantCommand request, CancellationToken cancellationToken)
        {
            using var context = new DynamoDBContext(_dynamoDb);
            var participant = await context.LoadAsync<Participant>(nameof(Participant), $"{request.EventId}-{request.ParticipantId}", new DynamoDBOperationConfig
            {
                ConsistentRead = true
            }, cancellationToken);
            if (participant == null)
                throw new NotFoundException();

            participant.Status = ParticipantStatus.Started;
            participant.StartDate = DateTime.UtcNow;

            await context.SaveAsync(participant, cancellationToken);

            return Unit.Value;
        }
    }
}