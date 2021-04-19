using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using backend.business.Infrastructure;
using backend.domain;
using MediatR;

namespace backend.business.Participants.Commands.Finish
{
    public class FinishParticipantCommandHandler : IRequestHandler<FinishParticipantCommand>
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public FinishParticipantCommandHandler(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }
        public async Task<Unit> Handle(FinishParticipantCommand request, CancellationToken cancellationToken)
        {
            using var context = new DynamoDBContext(_dynamoDb);
            var participant = await context.LoadAsync<Participant>(nameof(Participant), $"{request.EventId}-{request.ParticipantId}", new DynamoDBOperationConfig
            {
                ConsistentRead = true
            }, cancellationToken);
            if (participant == null)
                throw new NotFoundException();

            participant.Status = ParticipantStatus.Finished;
            await context.SaveAsync(participant, cancellationToken);

            return Unit.Value;
        }
    }
}