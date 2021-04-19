using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using backend.business.Participants.Models;
using backend.domain;
using MediatR;

namespace backend.business.Participants.Commands.Register
{
    public class RegisterParticipantCommandHandler : IRequestHandler<RegisterParticipantCommand, ParticipantDetailModel>
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IMapper _mapper;

        public RegisterParticipantCommandHandler(IAmazonDynamoDB dynamoDb, IMapper mapper)
        {
            _dynamoDb = dynamoDb;
            _mapper = mapper;
        }
        public async Task<ParticipantDetailModel> Handle(RegisterParticipantCommand request, CancellationToken cancellationToken)
        {
            using var context = new DynamoDBContext(_dynamoDb);
            var toInsertParticipant = Participant.BuildParticipantForEvent(request.EventId, request.Name);
            await context.SaveAsync(toInsertParticipant, cancellationToken);
            return _mapper.Map(toInsertParticipant, new ParticipantDetailModel());
        }
    }
}