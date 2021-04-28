using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using backend.business.Events.Models;
using backend.domain;
using MediatR;

namespace backend.business.Events.Queries.GetList
{
    public class GetEventsListQueryHandler : IRequestHandler<GetEventsListQuery, IEnumerable<EventListModel>>
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IMapper _mapper;

        public GetEventsListQueryHandler(IAmazonDynamoDB dynamoDb, IMapper mapper)
        {
            _dynamoDb = dynamoDb;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EventListModel>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
        {
            var results = new List<Event>();

            using var context = new DynamoDBContext(_dynamoDb);
            var response = context.FromQueryAsync<Event>(new QueryOperationConfig
            {
                AttributesToGet = new List<string> { nameof(Event.Id), nameof(Event.Name), nameof(Event.StartDate), nameof(Event.EndDate), nameof(Event.City) },
                Select = SelectValues.SpecificAttributes,
                KeyExpression = new Expression
                {
                    ExpressionStatement = "Id > :id AND DomainType = :domainType",
                    ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry> { { ":id", new Primitive("0") }, { ":domainType", new Primitive(nameof(Event)) } }
                }
            });
            results.AddRange(await response.GetRemainingAsync(cancellationToken));

            return results.Select(x => _mapper.Map(x, new EventListModel()));
        }
    }
}