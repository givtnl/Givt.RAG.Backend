using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using backend.business.Backers.Models;
using backend.domain;
using MediatR;

namespace backend.business.Backers.Queries.GetList
{
    public class GetBackersListQueryHandler : IRequestHandler<GetBackersListQuery, IEnumerable<BackerListModel>>
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IMapper _mapper;

        public GetBackersListQueryHandler(IAmazonDynamoDB dynamoDb, IMapper mapper)
        {
            _dynamoDb = dynamoDb;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BackerListModel>> Handle(GetBackersListQuery request, CancellationToken cancellationToken)
        {
            var results = new List<Backer>();

            using var context = new DynamoDBContext(_dynamoDb);
            var response = context.FromQueryAsync<Backer>(new QueryOperationConfig
            {
                AttributesToGet = new List<string> { nameof(Backer.Id), nameof(Backer.Name), nameof(Backer.EmailAddress), nameof (Backer.Amount) },
                Select = SelectValues.SpecificAttributes,
                KeyExpression = new Expression
                {
                    ExpressionStatement = "begins_with(Id,:eventId) AND DomainType = :domainType",
                    ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry> { { ":eventId", new Primitive($"{request.EventId}-{request.ParticipantId}") }, { ":domainType", new Primitive(nameof(Backer)) } }
                }
            });
            results.AddRange(await response.GetRemainingAsync(cancellationToken));

            return results.Select(x => _mapper.Map(x, new BackerListModel()));
        }
    }
}