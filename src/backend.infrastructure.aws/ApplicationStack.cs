using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.SQS;

namespace backend.infrastructure.aws
{
    public class ApplicationStack : Stack
    {
        public HackatonStackProperties Props { get; }

        public ApplicationStack(Construct scope, string id, HackatonStackProperties props) : base(scope, id, props)
        {
            var environmentName = new CfnParameter(this, "EnvironmentName", new CfnParameterProps
            {
                Type = "String",
                Default = "",
                Description = "The name of the environment to push the resources in"
            });


            Props = props;
            CreateS3Bucket(environmentName.ValueAsString);
            CreateEventsTable(environmentName.ValueAsString);
            CreateParticipantsTable(environmentName.ValueAsString);
            CreateBackersTable(environmentName.ValueAsString);

            CreateBackerNotifyParticipantFinishedQueue(environmentName.ValueAsString);
        }

        private void CreateS3Bucket(string environmentNameValueAsString)
        {
            new Bucket(this, "attachmentsbucket", new BucketProps
            {
                AccessControl = BucketAccessControl.PRIVATE,
                BlockPublicAccess = BlockPublicAccess.BLOCK_ALL,
                Encryption = BucketEncryption.S3_MANAGED,
                EnforceSSL = true,
                Versioned = true,
                PublicReadAccess = false
            });
        }

        private void CreateBackerNotifyParticipantFinishedQueue(string environmentName)
        {
            new Queue(this, "participantfinishedqueue", new QueueProps
            {
                QueueName = $"{environmentName}ParticipantFinishedQueue",
                ReceiveMessageWaitTime = Duration.Seconds(20),
                DeliveryDelay = Duration.Seconds(0),
                VisibilityTimeout = Duration.Seconds(60),
                RetentionPeriod = Duration.Days(7)
            });
        }

        private void CreateBackersTable(string environmentName)
        {
            new Table(this, "backerstable", new TableProps
            {
                BillingMode = BillingMode.PAY_PER_REQUEST,
                Encryption = TableEncryption.DEFAULT,
                TableName = $"{environmentName}Backers",
                RemovalPolicy = RemovalPolicy.DESTROY,
                PartitionKey = new Attribute { Name = "DomainType", Type = AttributeType.STRING },
                SortKey = new Attribute { Name = "Id", Type = AttributeType.STRING }
            });
        }

        private void CreateParticipantsTable(string environmentName)
        {
            new Table(this, "participantstable", new TableProps
            {
                BillingMode = BillingMode.PAY_PER_REQUEST,
                Encryption = TableEncryption.DEFAULT,
                RemovalPolicy = RemovalPolicy.DESTROY,
                TableName = $"{environmentName}Participants",
                PartitionKey = new Attribute { Name = "DomainType", Type = AttributeType.STRING },
                SortKey = new Attribute { Name = "Id", Type = AttributeType.STRING }
            });
        }

        private void CreateEventsTable(string environmentName)
        {
            new Table(this, "eventstable", new TableProps
            {
                BillingMode = BillingMode.PAY_PER_REQUEST,
                Encryption = TableEncryption.DEFAULT,
                RemovalPolicy = RemovalPolicy.DESTROY,
                TableName = $"{environmentName}Events",
                PartitionKey = new Attribute { Name = "DomainType", Type = AttributeType.STRING },
                SortKey = new Attribute { Name = "Id", Type = AttributeType.STRING }
            });
        }
    }
}