using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using backend.business.Events.Models;
using backend.business.Participants.Models;
using backend.domain;
using MediatR;

namespace backend.business.Participants.Queries.GetList
{
    public class GetParticipantsListQueryHandler : IRequestHandler<GetParticipantsListQuery, IEnumerable<ParticipantListModel>>
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IMapper _mapper;

        public GetParticipantsListQueryHandler(IAmazonDynamoDB dynamoDb, IMapper mapper)
        {
            _dynamoDb = dynamoDb;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ParticipantListModel>> Handle(GetParticipantsListQuery request, CancellationToken cancellationToken)
        {
            var results = new List<Participant>();

            using var context = new DynamoDBContext(_dynamoDb);
            var response = context.FromQueryAsync<Participant>(new QueryOperationConfig
            {
                AttributesToGet = new List<string> { nameof(Participant.Id), nameof(Participant.Name) },
                Select = SelectValues.SpecificAttributes,
                KeyExpression = new Expression
                {
                    ExpressionStatement = "begins_with(Id,:eventId) AND DomainType = :domainType",
                    ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry> { { ":eventId", new Primitive(request.EventId) }, { ":domainType", new Primitive(nameof(Participant)) } }
                }
            });
            results.AddRange(await response.GetRemainingAsync(cancellationToken));

            return results.Select(x => _mapper.Map(x, new ParticipantListModel()));
        }
    }
}