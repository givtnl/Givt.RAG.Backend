using Amazon.CDK;

namespace backend.infrastructure.aws
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App();

            var applicationStack = new ApplicationStack(app, "ApplicationStack", new HackatonStackProperties());
            app.Synth();
        }
    }
}
