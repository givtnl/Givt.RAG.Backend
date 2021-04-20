using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using backend.business.Backers.Models;
using backend.domain;
using MediatR;

namespace backend.business.Backers.Queries.GetDetail
{
    public class GetBackerDetailQueryHandler : IRequestHandler<GetBackerDetailQuery, BackerDetailModel>
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IMapper _mapper;

        public GetBackerDetailQueryHandler(IAmazonDynamoDB dynamoDb, IMapper mapper)
        {
            _dynamoDb = dynamoDb;
            _mapper = mapper;
        }
        public async Task<BackerDetailModel> Handle(GetBackerDetailQuery request, CancellationToken cancellationToken)
        {
            using var context = new DynamoDBContext(_dynamoDb);
            var result = await context.LoadAsync<Backer>(nameof(Backer), $"{request.EventId}-{request.ParticipantId}-{request.Id}", cancellationToken);
            return _mapper.Map(result, new BackerDetailModel());
        }
    }
}