using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using backend.business.Events.Models;
using backend.business.Participants.Models;
using backend.domain;
using MediatR;

namespace backend.business.Participants.Queries.GetDetail
{
    public class GetParticipantDetailQueryHandler : IRequestHandler<GetParticipantDetailQuery, ParticipantDetailModel>
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IMapper _mapper;

        public GetParticipantDetailQueryHandler(IAmazonDynamoDB dynamoDb, IMapper mapper)
        {
            _dynamoDb = dynamoDb;
            _mapper = mapper;
        }
        public async Task<ParticipantDetailModel> Handle(GetParticipantDetailQuery request, CancellationToken cancellationToken)
        {
            using var context = new DynamoDBContext(_dynamoDb);
            var result = await context.LoadAsync<Participant>(nameof(Participant),$"{request.EventId}-{request.Id}", cancellationToken);
            return _mapper.Map(result, new ParticipantDetailModel());
        }
    }
}