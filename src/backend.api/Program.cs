using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHost((host) =>
                {
                    host.ConfigureKestrel(x => x.AddServerHeader = false);
                })
            .ConfigureAppConfiguration((hostBuilder, configBuilder) =>
            {
                configBuilder.AddJsonFile("appsettings.json", true);
                configBuilder.AddJsonFile($"appsettings.{hostBuilder.HostingEnvironment.EnvironmentName}.json", true);
                configBuilder.AddEnvironmentVariables();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
