namespace backend.business.Infrastructure
{
    public class ApplicationSettings
    {
        public string TablePrefix { get; set; }
        public string ParticipantFinishedQueue { get; set; }
        public string PaymentProviderApiKey { get; set; }
        public string BucketName { get; set; }
    }
}