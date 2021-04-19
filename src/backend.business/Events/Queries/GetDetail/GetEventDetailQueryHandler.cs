using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using backend.business.Events.Models;
using backend.domain;
using MediatR;

namespace backend.business.Events.Queries.GetDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailModel>
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(IAmazonDynamoDB dynamoDb, IMapper mapper)
        {
            _dynamoDb = dynamoDb;
            _mapper = mapper;
        }
        public async Task<EventDetailModel> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            using var context = new DynamoDBContext(_dynamoDb);
            var result = await context.LoadAsync<Event>(nameof(Event), request.Id, cancellationToken);
            return _mapper.Map(result, new EventDetailModel());
        }
    }
}