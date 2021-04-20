using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using backend.business.Backers.Models;
using backend.domain;
using MediatR;

namespace backend.business.Backers.Commands.Register
{
    public class RegisterBackerCommandHandler : IRequestHandler<RegisterBackerCommand, BackerDetailModel>
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IMapper _mapper;

        public RegisterBackerCommandHandler(IAmazonDynamoDB dynamoDb, IMapper mapper)
        {
            _dynamoDb = dynamoDb;
            _mapper = mapper;
        }
        public async Task<BackerDetailModel> Handle(RegisterBackerCommand request, CancellationToken cancellationToken)
        {
            using var context = new DynamoDBContext(_dynamoDb);
            var toInsertBacker = Backer.BuildForParticipant(request.EventId,request.ParticipantId, request.Name, request.Amount, request.EmailAddress);
            await context.SaveAsync(toInsertBacker, cancellationToken);
            return _mapper.Map(toInsertBacker, new BackerDetailModel());
        }
    }
}